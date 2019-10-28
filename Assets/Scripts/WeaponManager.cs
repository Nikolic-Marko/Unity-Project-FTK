using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour {

	[SerializeField]
	private string weaponLayerName = "Rifle";

	[SerializeField]
	private Transform weaponHolder;

	[SerializeField]
	private PlayerWeapon weaponOne;

	private PlayerWeapon currentWeapon;


	void Start()
	{
		EquipWeapon(weaponOne);
	} 

	public PlayerWeapon GetCurrentWeapon()
	{
		return currentWeapon;
	}


	void EquipWeapon (PlayerWeapon _weapon)
	{
		currentWeapon = _weapon;

		GameObject _weaponIns = (GameObject)Instantiate (_weapon.graphics, weaponHolder.position, weaponHolder.rotation);
		_weaponIns.transform.SetParent (weaponHolder);
		if (isLocalPlayer)
			_weaponIns.layer = LayerMask.NameToLayer (weaponLayerName);
	}
		

}
