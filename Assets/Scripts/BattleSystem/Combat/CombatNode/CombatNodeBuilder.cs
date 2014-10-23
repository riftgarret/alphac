// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
public class CombatNodeBuilder
{
	private CombatNodeFactory mFactory;

	// if weapon is provided, weapon index is ignored	
	private int mWeaponIndex;
    private bool mUseEquipment;
    private bool mUseBuffs;

	private SkillCombatNode mSkillCombatNode;

	public CombatNodeBuilder (CombatNodeFactory factory)
	{
		this.mFactory = factory;
		this.mWeaponIndex = -1;
        this.mUseBuffs = true;
        this.mUseEquipment = true;
	}

	/// <summary>
	/// Sets the index of the weapon. This is set to 0 by default.
	/// </summary>
	/// <param name="weaponIndex">Weapon index.</param>
	public CombatNodeBuilder SetWeaponIndex(int weaponIndex) {
		mWeaponIndex = weaponIndex;
		return this;
	}


	public CombatNodeBuilder SetSkillCombatNode(CombatRound round) {
        mSkillCombatNode = new SkillCombatNode(round);
		return this;
	}

    public CombatNodeBuilder SetUseEquipment(bool allow) {
        mUseEquipment = allow;
        return this;
    }

    public CombatNodeBuilder SetUseBuffs(bool allow) {
        mUseBuffs = allow;
        return this;
    }

	public CompositeCombatNode Build() {
		// build composite for character
		CompositeCombatNode rootNode = new CompositeCombatNode ();
		// child node
		rootNode.AddNode (mFactory.CreateCharacterNode());

        // use all equipment
        if (mUseEquipment) {
            // weaopns
            for (int i = 0; i < mFactory.entity.equipedWeapons.Length; i++) {
                rootNode.AddNode(mFactory.CreateWeaponConfigNode(i, i == mWeaponIndex));
            }

            // armor
            for (int i = 0; i < mFactory.entity.equipedArmor.Length; i++) {
                rootNode.AddNode(mFactory.CreateArmorNode(i));
            }
        }

        if (mUseBuffs) {
            // TOODO
        }


		if (mSkillCombatNode != null) {
			rootNode.AddNode (mSkillCombatNode);
		}

		// TODO iterate through buffs and equipment
		return rootNode;
	}

	/// <summary>
	/// Build the combat resolver directly
	/// </summary>
	/// <returns>The resolver.</returns>
	public CombatResolver BuildResolver() {
		return new CombatResolver(mFactory.entity, Build());
	}
}
