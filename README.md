
---

# Unity Rotation Control System — Documentation & Setup Guide

---

## 1. Script: RotatingDisplay.cs

### Purpose:

Controls the rotation of a GameObject with two main modes:

* **Idle rotation:** Automatically spins slowly around Y-axis (horizontal).
* **User rotation:** Allows user-controlled rotation via UI buttons (Up, Down, Left, Right).

### Key Variables:

| Variable Name              | Type  | Description                                    |
| -------------------------- | ----- | ---------------------------------------------- |
| idleRotationSpeed          | float | Speed of automatic idle rotation (deg/sec).    |
| userRotationSpeed          | float | Speed of user-driven rotation (deg/sec).       |
| targetRotationX/Y          | float | Target rotation angles (vertical/horizontal).  |
| currentRotationX/Y         | float | Smoothed current rotation angles.              |
| rotatingUp/Down/Left/Right | bool  | Flags to track user input rotation directions. |

### Core Logic (Update method):

* If no horizontal input, rotate slowly around Y (idle).
* If user input detected, adjust rotation accordingly.
* Clamp vertical rotation to prevent flipping.
* Smoothly interpolate current rotation toward target rotation.
* Apply rotation to GameObject.

### User Input Methods:

These are called by UI buttons on press and release events.

```csharp
public void OnPressUp()    => rotatingUp = true;
public void OnReleaseUp()  => rotatingUp = false;
public void OnPressDown()  => rotatingDown = true;
public void OnReleaseDown()=> rotatingDown = false;
public void OnPressLeft()  => rotatingLeft = true;
public void OnReleaseLeft()=> rotatingLeft = false;
public void OnPressRight() => rotatingRight = true;
public void OnReleaseRight()=> rotatingRight = false;
```

### How to Use:

* Attach this script to the object you want to rotate.
* Configure the rotation speeds in inspector.
* Connect UI buttons to call the input methods on press/release.

---

## 2. Script: SetupRotationButtons.cs

### Purpose:

Automates the wiring of UI buttons to the `RotatingDisplay` input methods using EventTrigger components.

### How It Works:

* Finds buttons by name: `ButtonUp`, `ButtonDown`, `ButtonLeft`, `ButtonRight`.
* Adds or clears existing EventTrigger components.
* Sets up `PointerDown` and `PointerUp` events on each button to call corresponding `OnPressX()` and `OnReleaseX()` methods on `RotatingDisplay`.

### How to Use:

* Attach this script to any GameObject in the scene (e.g., an empty GameObject).
* Assign the reference to the `RotatingDisplay` component in the inspector.
* Ensure buttons are named exactly as above.
* Run the scene, and the buttons will control rotation automatically.

---

## 3. Unity Scene Setup Guide

### Step 1: Prepare Rotating Object

* Add your 3D model or trophy GameObject in the scene.
* Attach the `RotatingDisplay` script to it.
* Adjust `idleRotationSpeed` and `userRotationSpeed` as desired (defaults: 10 and 100).

### Step 2: Setup UI Canvas and Buttons

* In **Hierarchy**, right-click → UI → Canvas (if none exists).

* Inside the Canvas, create a **Panel** to hold buttons:

  * Rename it `DPadPanel`.
  * Anchor it to bottom-left corner (click Anchor presets, hold Alt, click bottom-left).
  * Resize panel to about 200x200 px.
  * Set panel Image alpha to \~0.3 for transparency.

* Create 4 Buttons as children of `DPadPanel`:

  * Rename buttons to `ButtonUp`, `ButtonDown`, `ButtonLeft`, `ButtonRight`.
  * Arrange them in a D-Pad shape inside the panel:

    * `ButtonUp`: center top, y = +50
    * `ButtonDown`: center bottom, y = -50
    * `ButtonLeft`: left center, x = -50
    * `ButtonRight`: right center, x = +50
  * Set each button’s Image alpha to \~0.4 for slight transparency.
  * (Optional) Add arrow sprites to buttons for visual clarity.

### Step 3: Setup Button Event Wiring

* Create an empty GameObject in the scene, name it `RotationButtonManager`.
* Attach the `SetupRotationButtons` script to it.
* In the inspector, drag your rotating GameObject (with `RotatingDisplay`) into the `rotatingDisplay` field.

### Step 4: Play and Test

* Press Play in Unity.
* Hold each button and observe your GameObject rotate accordingly.
* When no horizontal input, it slowly spins on its own.

---

## 4. Tips & Notes

* Button names **must match exactly** for the automatic wiring to work.
* Adjust rotation speeds to tune feel and responsiveness.
* You can customize UI button appearance with your own sprites and styles.
* To improve UX on mobile, make buttons large enough and spaced apart.
* Use the transparency settings to avoid obstructing main content.

---
