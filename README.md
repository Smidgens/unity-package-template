# 📦 Unity Package Template

Project template for creating a custom package for Unity. Uses conventions recommended by Unity's official documentation on packages (see links below).

Also included is a basic package export tool for compressing packages to archives.

### Features

- [x] [Zip Export](https://github.com/Smidgens/unity-package-template/milestone/4)
- [ ] [Tarball Export](https://github.com/Smidgens/unity-package-template/milestone/1)
- [ ] [Package Linting](https://github.com/Smidgens/unity-package-template/milestone/2)
- [ ] [Version Bump and Git Tags](https://github.com/Smidgens/unity-package-template/milestone/3)
- [ ] [Password Compression]()
 
**Notes**:
* The project is set to `2020.3` but this isn't a requirement (so long as the version you're using isn't ancient or pre package system).
* The project is almost completedly stripped of the usual package dependencies Unity adds by default for new projects so you'll have to go through the Package Manager UI and manually enable those you need.

<br/><br/>


# ⚙️ Setup

* Clone project (template, copy etc.)
* Rename `custom-package` under `Packages` folder in project root to a valid (per Unity conventions) package name of your choice.
* Change metadata in `package.json` and add more if needed (see link to unity docs on manifest metadata below).
* Rename assembly files in the package folder to match the new name you chose, and remove any folders you won't be using (Editor, Tests etc.).

<br/>

# 🗳️ Export

* Open Package Exporter
  * `Window / Package Exporter`
* Select an **Export Profile** in the list (or create your own)
  * Make sure the package name set in the profile matches the name you cose (press `View` to edit profile).
* Press `Export` to compress package folder to an archive (default saved to `Export` folder in the project root).


<br/>

# 🔗 See Also

* [Custom Packages](https://docs.unity3d.com/Manual/CustomPackages.html)
* [Package Manifest](https://docs.unity3d.com/Manual/upm-manifestPkg.html)
* [Package Layout](https://docs.unity3d.com/Manual/cus-layout.html)
