# Game Prototype - README

## Overview
This prototype is a **3D platformer and top-down exploration game** developed in Unity. It focuses on testing core player mechanics before expanding into full gameplay development. The current version includes movement, jumping, dashing, special abilities, and an attack system with unique logical and emotional states.

## Features
### 1. Player Movement (Platformer Section)
- **Basic movement** using `PlayerMovement.cs`.
- **Variable jump height** controlled by `PlayerJump.cs`.
- **Air dashing** system (`PlayerDash.cs`) with a meter system.

### 2. Combat System
#### Basic Combat Abilities
- Players use **clock magic** to attack enemies with normal attacks.
- Press the **Attack button** or **left-click** to perform basic attacks.
- **Charging** expands the attack area and causes a powerful explosion with high damage.

#### Support Skill Sets
Players can use three support skill sets, appearing sequentially in an order chosen by the player:

##### 1. Logical (Vision and Laser Gun Attacks)
**Exploration Mode (Cooldown: 15s):**
- Increases player vision by **25-50%**.
- When charging, players can exchange **50% Mana** to activate "Expansion Mode," which grants:
  - **Extended attack range**.
  - **Marked enemies** (for the first 5 seconds) become "Bound" (immobile) for **1 minute**.
  - Attacking marked enemies restores **1 Mana** (up to 5 times).

**Combat Mode (Cooldown: 7s):**
- Locks onto **up to 5 enemies** within range, prioritizing "Bound" enemies.
- Upon attack, **five star projectiles** strike locked targets.
- Lock duration is limited; players must manually aim using a virtual joystick or mouse.

##### 2. Emotion (Puzzle Assistance and Counterattacks)
**Exploration Mode (Cooldown: 8s):**
- Near a puzzle, players can activate **"Wire Connection"** to consume **10 Mana** and detect clues via "scent."
- If the clue is nearby, it is highlighted. Otherwise, the **map marks the location** for **5 minutes**.

**Combat Mode (Cooldown: 10s):**
- Activates a **counter-shield**, consuming **15 Mana**.
  - If close to an enemy, reflects projectiles.
  - If no enemy shoots, activates **"Isolated Lava"**, trapping up to **5 enemies**:
    - **50% chance** to recruit captured monsters as combat allies (**75% base HP**).
    - If not recruited, trapped enemies can be thrown for damage.

##### 3. Instinct (Time and Space Control)
**Exploration Mode (Cooldown: 30s, Duration: 3-5s):**
- Activates **"Stasis"**, freezing the environment for **3-5s**.
- Marks special items (books, chests, interactable objects).
- In **real-world mode**, "Stasis" becomes **"Perception"**, displaying three zones:
  - **Green:** Safe zone for collecting clues.
  - **Red:** High-risk areas with **10+ enemies**, obstructing exploration.
  - **Yellow:** Moderate risk zones with **fewer than 10 scattered enemies**.
- In **Dream Precursor Mode**, players can activate **"Terrain Exploration"** to reveal shortcuts for **10 seconds**.

**Combat Mode:**
- Extends **Stasis** duration by **5s**, up to **15s** max.
- During Stasis, players can select one of two charge states:
  - **"Hibernation"**: Enemies **cannot attack for 1s**, stacking up to 3 times.
    - Each hit removes **0.5s per stack**.
    - At **3 stacks**, upgrades to **"Imprisonment"**, dealing damage based on **x% of base attack** (min: **5%**, max: **15%**).
  - **"Charge Forward"**:
    - All attacks in **3s** instantly **finish off enemies** below **5% HP**.
    - Increases **attack speed by 10%**, stacking by **8% per continued action**.
    - Killing an enemy **auto-locks** onto the nearest target and dashes up to **3 times consecutively**.
    - Attack speed boost persists for **5s** before reverting to **Stasis Mode**.
- Players can **choose their charge state** via skill icons or assigned keybinds.

**"Trade-Off Mechanic"** (Default for Instinct Set):
- Every **Charge Skill activation** triggers this effect.
- Players can sacrifice **20% of current HP** to use a charge skill, stacking **1 "Charge" layer**.
- At **3 "Charge" layers**, **"Bloodthirst"** activates:
  - **Reduces HP to 1** for **3s**.
  - **Boosts base attack by 150%**.
  - After **3s**, every hit restores **5% of lost HP**.
- **"Bloodthirst" lasts for 15s**, activating only **3 times per exploration.**

## How to Test the Prototype

### 1. Requirements
- **Unity** (Latest LTS version recommended)
- **Input Device**: Keyboard/Mouse or Controller
- **Debugger/Console**: Enabled to track player states and ability usage

### 2. Setup Instructions
1. **Clone the Repository**
   ```sh
   git clone <repository-link>
   cd <project-folder>
   ```
2. **Open in Unity**
   - Launch Unity and open the project folder.
3. **Run the Game**
   - Open the `PrototypeScene`.
   - Press **Play** in Unity to start testing.
4. **Enable Debug Mode**
   - A **Text Box Debugger** is available to track attack type changes.

### 3. Testing Checklist
- [ ] **Move & Jump**: Test basic movement and jumping behavior.
- [ ] **Wall Jump**: Verify charge mechanic works correctly.
- [ ] **Dash**: Ensure the air dash consumes and refills the meter.
- [ ] **Combat System**: Test primary and support skill sets.
- [ ] **Exploration Mode**: Verify skill interactions with puzzles and map elements.
- [ ] **Debug Messages**: Ensure attack type is displayed in the text box.

## Next Steps
- Implement animations for **combat skills and charge states**.
- Integrate top-down exploration mechanics.
- Optimize player movement and special abilities.

---
### Contributors
- **Caelus Ken** (`Khang Thuan`) - Developer & Designer
- **Shymastic** (`Dang Khoa`) - Developer

Feel free to report issues or suggest improvements!

