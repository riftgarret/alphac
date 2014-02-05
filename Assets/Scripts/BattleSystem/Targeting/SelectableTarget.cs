using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Single target to display as a selectable option, when targeting a skill
/// </summary>
public class SelectableTarget {
	public readonly string targetName;
	public readonly List<BattleEntity> targets;

	public SelectableTarget(string name, List<BattleEntity> targets) {
		this.targetName = name;
		this.targets = targets;
	}
}
