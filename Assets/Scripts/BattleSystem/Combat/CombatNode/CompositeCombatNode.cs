//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

public class CompositeCombatNode : ICombatNode
{
	private List<ICombatNode> mChildren;

	public CompositeCombatNode ()
	{
		mChildren = new List<ICombatNode>();
	}

	public void AddNode(ICombatNode combatNode) {
		mChildren.Add (combatNode);
	}

	public float GetProperty(CombatPropertyType property) {

		float total = 0;
		foreach(ICombatNode mod in mChildren) {
			total += mod.GetProperty(property);
		}
		return total;

	}

	public float GetPropertyScalar(CombatPropertyType property) {

		float total = 0;
		foreach(ICombatNode mod in mChildren) {
			total += mod.GetPropertyScalar(property);
		}
		return total;
	}

    public bool HasFlag(CombatFlag flag) {
        foreach (ICombatNode mod in mChildren) {
            if (mod.HasFlag(flag)) {
                return true;
            }
        }
        return false;
    }
}


