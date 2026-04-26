# Unity Configs

A lightweight ScriptableObject config system that cleanly separates *lookup keys* from *data* - no redundant fields, no mixed responsibilities.

## The Problem

In many Unity projects, configuration data ends up holding the same type that's used only for searching or identification. This forces objects to carry information they don't own, mixing lookup responsibility with data responsibility.

Unity Configs solves this. The search key (type) lives only at the config level. Data objects contain only what they're responsible for, with no knowledge of how they are found or selected.

## Why This Matters

- Enforces single responsibility for data objects
- Prevents redundant or misleading fields in data models
- Keeps runtime objects clean and focused
- Makes data reusable in different contexts
- Reduces coupling between logic and configuration

## Installation

There are several options to install this package.

### Unity Package Manager

Open Unity Package Manager and go to **Add package from git URL...** and paste:

```
https://github.com/ostrzolekpawel/UnityConfigs.git?path=Assets/Configs
```

### Manifest

Add the package directly to your `manifest.json`:

```json
{
    "dependencies": {
        "com.osirisgames.configs": "https://github.com/ostrzolekpawel/UnityConfigs.git?path=Assets/Configs"
    }
}
```

---

## Quick Start

The following example sets up a weapon config in five steps.

### Step 1 - Define a lookup key

```csharp
public enum WeaponType { Sword, Bow, Staff }
```

### Step 2 - Define a data class

```csharp
[System.Serializable]
public class WeaponData
{
    public int Damage;
    public float AttackSpeed;
    public string AnimationClipName;
}
```

### Step 3 - Create the config asset class

Inherit from `ConfigScriptable<TType, TData>`. That's the whole implementation.

```csharp
using Osiris.Configs;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/WeaponConfig")]
public class WeaponConfig : ConfigScriptable<WeaponType, WeaponData> { }
```

> `[CreateAssetMenu]` is optional but makes creating the asset from the Project window convenient.

### Step 4 - Create and fill the asset in Unity

1. Right-click in the Project window → **Create → Configs → WeaponConfig**
2. Name it `WeaponConfig` and select it in the Inspector
3. Set the **Default** value (used as a fallback when a key is not found)
4. Expand the **Data** list and add entries:

| Type  | Damage | Attack Speed | Animation Clip |
|-------|--------|--------------|----------------|
| Sword | 30     | 0.8          | sword_swing    |
| Bow   | 20     | 1.5          | bow_shoot      |
| Staff | 40     | 0.6          | staff_cast     |

> Unity's `OnValidate` will throw if you accidentally add duplicate keys.

### Step 5 - Use at runtime

```csharp
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private WeaponConfig _weaponConfig;
    [SerializeField] private WeaponType _equippedType = WeaponType.Sword;

    private void Start()
    {
        WeaponData stats = _weaponConfig.GetData(_equippedType);
        Debug.Log($"Equipped: {_equippedType} - {stats.Damage} dmg @ {stats.AttackSpeed} speed");
    }
}
```

Assign `WeaponConfig` to the field in the Inspector and you're done.

---

## API Reference

### `IConfig<TType, TData>`

| Member | Description |
|--------|-------------|
| `TData Default` | Fallback value returned when the key is not found |
| `TData GetData(TType type)` | Returns data mapped to `type`, or `Default` if missing |

### `ConfigScriptable<TType, TData>`

Abstract `ScriptableObject` base class implementing `IConfig<TType, TData>`. Inherit from it to create concrete config assets. `OnValidate()` rejects duplicate keys at edit time so misconfiguration fails fast in the Editor, not at runtime.
