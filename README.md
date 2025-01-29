# Virtual Pet Companion - Unity Project

## Overview
A virtual pet companion app built with Unity, featuring an AI-powered pet that provides emotional support and interactive experiences.

## Prerequisites
- Unity 2022.3.16f1 LTS (recommended for long-term stability)
- Visual Studio 2019/2022 or JetBrains Rider
- Git LFS (for handling large assets)

## Project Structure
```
Assets/
├── Animations/        # Animation controllers and clips
├── Input/            # Input action assets
├── Prefabs/          # Reusable game objects
├── Scenes/           # Unity scenes
├── Scripts/          # C# scripts
├── Sprites/          # 2D graphics (placeholders)
└── StreamingAssets/  # Runtime loadable content
```

## Quick Start
1. Clone the repository
2. Open Unity Hub
3. Add project from disk
4. Select Unity 2022.3.16f1 LTS
5. Open the project
6. Load `Assets/Scenes/Main.unity`

## Input System Setup
The project uses Unity's new Input System package for cross-platform input handling:
- Touch input for mobile
- Mouse/Keyboard for desktop
- Gamepad support (optional)

## Scene Organization
1. **Main Menu** (`Assets/Scenes/MainMenu.unity`)
   - Entry point
   - Settings access
   - Profile management

2. **Pet Scene** (`Assets/Scenes/PetScene.unity`)
   - Main interaction area
   - Chat interface
   - Activity tracking

3. **Customization** (`Assets/Scenes/Customization.unity`)
   - Pet customization
   - Accessory management
   - Background selection

## Build Configuration
### Android
- Minimum API Level: Android 7.0 (API 24)
- Target API Level: Android 13 (API 33)
- Scripting Backend: IL2CPP
- Target Architectures: ARM64, ARMv7

### iOS
- Minimum Version: iOS 13.0
- Target SDK: Latest
- Scripting Backend: IL2CPP
- Target Architectures: ARM64

### WebGL
- Compression Format: Brotli
- Development Build: Disabled for production
- Exception Support: Disabled for production

## Development Guidelines
1. **Code Style**
   - Follow C# naming conventions
   - Use [SerializeField] for inspector variables
   - Implement interfaces for modularity

2. **Performance**
   - Use object pooling for UI elements
   - Implement proper cleanup
   - Profile regularly

3. **Asset Guidelines**
   - Texture sizes: Power of 2
   - Sprite atlas usage
   - Compress audio appropriately

## Testing
1. Test on target platforms regularly
2. Use Unity's Device Simulator
3. Profile performance on low-end devices

## Version Control
- Use Unity Smart Merge
- Configure Git LFS for binary files
- Regular project backup

## Additional Notes
- Unity Version: 2022.3.16f1 LTS
- Required Packages:
  - Input System
  - TextMeshPro
  - 2D Sprite
  - Universal RP