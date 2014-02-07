using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class EnemySkillSet : ScriptableObject {
	[SerializeField]
	private AISkillRule [] mSkillRules;
}
