using Pryanik.Layout;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Pryanik.Progress
{
    [Serializable]
    public struct LevelProgress
    {
        public int normalTriesCount;
        public int practiceTriesCount;

        public float progress;
    }
    
    public interface IWriter
    {
        void Write();
    }

    public interface IProgressSaver
    {
        LevelProgress GetProgress(string levelId);
        void SetProgress(string id, LevelProgress progress);
        void WriterSubscribe(IWriter writer);
        void WriterUnSubscribe(IWriter writer);
    }
    public class ProgressSaver : IProgressSaver
    {
        private event Action _onQuit;
        private readonly Dictionary<string, LevelProgress> _idToProgress = new Dictionary<string, LevelProgress>();
        static string Path => Application.persistentDataPath+"/";

        private readonly LayoutStorage _levels;

        public ProgressSaver(LayoutStorage storage)
        {
            _levels = storage;
            ReadAll();
            Application.quitting += OnQuit;
        }

        public LevelProgress GetProgress(string id) => _idToProgress[id];


        public void SetProgress(string id, LevelProgress progress)
        {
            _idToProgress[id] = progress;
        }

        void OnQuit()
        {
            _onQuit?.Invoke();
            WriteAll();
        }

        #region Reader
        public void ReadAll()
        {
            foreach(var id in _levels.GetAll().Select(x => x.Id))
            {
                var dat = ReadLevel(id);
               
                _idToProgress.Add(id, dat);
            }
        }
        LevelProgress ReadLevel(string id)
        {

            string path = Path+id+".dat"; 
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                using (stream)
                {
                    LevelProgress data = (LevelProgress)formatter.Deserialize(stream);

                    return data;
                }
            }
            else
                return new LevelProgress();
        }
        #endregion

        #region Writer
        public void WriteAll()
        {
            foreach (var id in _levels.GetAll().Select(x => x.Id))
            {
                WriteLevel(id);
            }
        }
        void WriteLevel(string id)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Path+id+".dat";
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
            using (stream)
            {
                stream.SetLength(0);
                formatter.Serialize(stream, _idToProgress[id]);
            }
        }

        public void WriterSubscribe(IWriter writer)
        {
            _onQuit += writer.Write;
        }

        public void WriterUnSubscribe(IWriter writer)
        {
            _onQuit -= writer.Write;
        }
        #endregion
    }
}