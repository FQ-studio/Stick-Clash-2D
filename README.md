# Stick Clash 2D

Original 2D stick-fighter prototype built with C# and Unity.

## Build Android APK

GitHub Actions workflow: **Unity Android Build** (`.github/workflows/unity-android.yml`). It uses the Unity Editor in a headless cloud build container via GameCI; no local computer is needed.

The repository owner must configure a valid Unity license in GitHub Actions secrets as `UNITY_LICENSE` before the licensed Unity build can succeed. Never commit license files, passwords, or tokens.

Open the repository's **Actions** tab, select **Unity Android Build**, and choose **Run workflow**. When successful, download the `Stick-Clash-2D-Android-APK` artifact from the run.

## Controls

- Player 1: A/D to move, W to jump, F to attack.
- Player 2: Left/Right arrows to move, Up to jump, Enter to attack.
- Touch buttons are also shown for mobile.
