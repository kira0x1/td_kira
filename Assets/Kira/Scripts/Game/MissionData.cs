using System.Runtime.Serialization;
using UnityEngine;

namespace Kira
{
    [CreateAssetMenu(fileName = "New Mission", menuName = "Kira/Mission")]
    public class MissionData : ScriptableObject
    {
        public string Title;
        public int SceneId;
        public Sprite Icon;
        public DifficultyLevel Difficulty;
    }

    public enum DifficultyLevel
    {
        [EnumMember(Value = "Easy")]
        EASY,
        [EnumMember(Value = "Normal")]
        NORMAL,
        [EnumMember(Value = "Hard")]
        HARD,
        [EnumMember(Value = "Very Hard")]
        VERY_HARD
    }
}