# Unity Configs

In many Unity projects, configuration data often contains the same type that is only used for searching or identification.
This leads to objects holding information they don’t actually need, mixing lookup responsibility with data responsibility.

This plugin separates those concerns.

The search key (type) exists only at the configuration level and is used solely for lookup.
The data objects themselves contain only the data they are responsible for, without knowledge of how they are found or selected.

## Why This Matters

- Enforces single responsibility for data objects

- Prevents redundant or misleading fields in data models

- Keeps runtime objects clean and focused

- Makes data reusable in different contexts

- Reduces coupling between logic and configuration

## Installation

There is several options to install this package:
- UPM
- directly in manifest

### Unity Package Manager

Open Unity Package Manager and go to **Add package from git URL...** and paste [https://github.com/ostrzolekpawel/UnityConfigs.git?path=Assets/Configs](https://github.com/ostrzolekpawel/UnityConfigs.git?path=Assets/Configs)

### Manifest
Add link to package from repository directly to manifest.json

**Example**
```json
{
    "dependencies": {
        // other packages
        // ...
        "com.osirisgames.configs": "https://github.com/ostrzolekpawel/UnityConfigs.git?path=Assets/Configs"
    }
}
```