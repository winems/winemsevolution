# Deployment steps
- Bump the project version of:
  - `WineMsEvolutionCli`
  - `WineMsEvolutionGui`
  - `WineMsSetup`
  
## Build
- Copy the required version of the Evolution SDK into the `EvolutionSdk\Current` folder.
- Rebuild the Solution.
- Build `WineMsSetup`
- Run `deploy\deploy.bat`
- Repeat the Build steps for each version of the SDK.