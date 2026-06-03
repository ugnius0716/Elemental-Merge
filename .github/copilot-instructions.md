# Copilot Instructions

## Project Guidelines
- User prefers game logic centralized in manager classes (e.g., BuildManager) rather than UI components; avoid putting core logic in UI elements like RadialMenu.
- Keep classes in separate files; do not merge unrelated classes into other files (e.g., do not place RadialMenuUI or BuildManager inside building Spaces.cs).