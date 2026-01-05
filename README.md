# 🤠 VR Quickdraw Simulator

**VR Quickdraw Simulator** is a virtual reality shooting game inspired by classic western duels.  
The player faces an enemy cowboy in a tense one-on-one standoff where **reaction time and aiming accuracy** decide the outcome: draw too early and you lose, react too slowly and you die — but reacting fast is not enough if you fail to aim and hit your target.

The project focuses on **timing, precision, and immersion**, using VR to heighten pressure and presence compared to a traditional screen-based game.

---

## 🧠 Concept

The core idea was to create a **simple but intense VR experience** that takes advantage of embodiment and presence.  
A western quickdraw duel is well-suited for VR because:

- The player must physically grab, aim, and shoot the weapon
- Timing alone is not sufficient — accurate aiming is required
- Enemy gaze and body orientation increase psychological pressure
- First-person perspective amplifies tension during short reaction windows

The goal was not to build a complex shooter, but a **tight, readable gameplay loop** centered on reaction time, precision, and clear rules.

---

## 🎯 Core Features

- **VR Gun Interaction**
  - Fully grabbable revolver using XR Interaction Toolkit
  - Physical trigger pull to shoot
  - Visible ammo represented by bullet models in the cylinder

- **Quickdraw Duel System**
  - Randomized wait time before the “DRAW!” signal
  - Early grabbing counts as a foul
  - Enemy shoots after a fixed reaction window if the player is too slow
  - Player must **aim at and hit the enemy** to win

- **Animated Enemy AI**
  - Enemy always faces the player
  - Draw, shoot, and death animations
  - Weapon visually swaps from holster to hand during draw

- **Reaction Time and Accuracy**
  - Reaction time is measured from the draw signal
  - A win only counts if the enemy is hit
  - Best reaction time is saved using PlayerPrefs

- **VR-Compatible UI**
  - In-world UI prompts (“Don’t grab yet”, “Shoot!”, win/loss messages)
  - VR main menu with controller-based interaction

---

## 🧱 Technical Stack

- **Engine:** Unity
- **XR Framework:** XR Interaction Toolkit
- **Platform:** Meta Quest (OpenXR)
- **Language:** C#
- **Rendering:** URP
- **Audio:** Spatialized sound effects

---

## 🏗️ How It Works

1. The scene starts in a waiting state.
2. The enemy stands idle while a random delay runs.
3. If the player grabs the gun too early, the duel is lost.
4. When the draw signal plays:
   - The enemy begins the draw animation
   - The player is allowed to grab, aim, and shoot
5. If the player hits the enemy first, they win.
6. If the enemy shoots first, or the player misses or fouls, the player loses.
7. The reaction time is calculated and compared to the best recorded score.

---

## 🧠 Learning Focus

This project helped me practice and understand:

- Translating traditional shooting mechanics into VR
- Combining reaction time with aiming accuracy in gameplay design
- VR interaction design and input handling
- Timing-based state management
- Animation events and visual synchronization
- Applying the Single Responsibility Principle across gameplay systems
- Designing readable and fair rules in VR

---

## 📸 Demo

- **VR Quickdraw Simulator video:**  
  https://drive.google.com/file/d/1SDOlnKsA1FRAVjnPXHZoSw7s93iGwXVm/view?usp=sharing

---

## 🧩 Assets Used

| Asset | Purpose | Link |
|------|--------|------|
| **Polygon Western Pack (Synty)** | Western environment and enemy character | https://assetstore.unity.com/packages/3d/environments/historic/polygon-western-pack-art-by-synty-112212 |
| **Dan Wesson Model 715** | Revolver gun model | https://assetstore.unity.com/packages/3d/props/weapons/dan-wesson-model-715-72033 |
| **Mixamo** | Enemy animations (idle, draw/shoot, death) | https://www.mixamo.com |
| **Pixabay** | Gunshot, bell, and duel sound effects | https://pixabay.com |

---

## 👨‍💻 Author

**Apurva Mishra**  
Developed as part of the **XRD course** at VIA University College.  
Focuses on VR interaction, timing-based gameplay, and immersive player feedback.

---
