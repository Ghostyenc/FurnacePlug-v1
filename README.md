# Furnace Plug

### Overview
`Furnace Plug` is a high-performance Rust Oxide plugin designed to modify the rates and speed of furnaces in the game. It is optimized to ensure minimal impact on server performance, even under heavy load. Configure fuel consumption, charcoal production, and smelting speed easily with this lightweight and efficient plugin.

---

### Features
- **High Performance**: Optimized to run efficiently without causing lag or performance drops.
- Adjustable fuel consumption rate for furnaces.
- Configurable charcoal production multiplier.
- Customizable smelting speed multiplier.

---

### Installation

1. Download the plugin file.
2. Place the `.cs` file into your server's `oxide/plugins` directory.
3. Restart the server or use the `oxide.reload FurnacePlug` command to load the plugin.

---

### Configuration

The plugin provides the following configuration options:

| **Option**                 | **Default Value** | **Description**                                  |
|----------------------------|-------------------|--------------------------------------------------|
| `FuelRateMultiplier`       | 1                 | Multiplies the fuel consumption rate.           |
| `CharcoalRateMultiplier`   | 2                 | Multiplies the charcoal production rate.        |
| `SmeltingSpeedMultiplier`  | 2.0               | Multiplies the smelting speed of furnaces.      |

#### Editing Configuration
1. Open the `oxide/config/FurnacePlug.json` file.
2. Modify the values as desired.
3. Save the file and reload the plugin with `oxide.reload FurnacePlug`.

---

### Performance Optimization

- **Efficient Patching**: Uses the Harmony library to patch only essential furnace-related methods.
- **Low Overhead**: The plugin is designed to work seamlessly in the background, ensuring no unnecessary resource consumption.
- **Scalable**: Tested to handle large servers with multiple furnaces without degradation in performance.

---

### ⚠️ Beware: Server Performance Warning

Setting rates too high *can* negatively impact server performance, especially on larger servers with many active furnaces. **If multipliers like `SmeltingSpeedMultiplier` or `CharcoalRateMultiplier` are excessively increased, it may lead to lag or instability.**  
- It is recommended to test changes on a test server before deploying them to a live server.
- Monitor performance after making adjustments to ensure smooth gameplay.
- In short, don't be silly with the rates..
  
---

### License
This plugin is exclusively licensed to **Enchanted.gg** and may not be edited or sold without explicit permission.

**© 2024 Ghosty & Enchanted.gg**
