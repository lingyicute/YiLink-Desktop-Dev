#!/bin/bash

Arch="$1"
OutputPath="$2"
Version="$3"

FileName="YiLink-${Arch}.zip"
wget -nv -O $FileName "https://github.com/lingyicute/YiLink-Desktop-core-bin/raw/refs/heads/master/$FileName"
7z x $FileName
cp -rf YiLink-${Arch}/* $OutputPath

PackagePath="YiLink-Package-${Arch}"
mkdir -p "$PackagePath/YiLink.app/Contents/Resources"
cp -rf "$OutputPath" "$PackagePath/YiLink.app/Contents/MacOS"
cp -f "$PackagePath/YiLink.app/Contents/MacOS/YiLink.icns" "$PackagePath/YiLink.app/Contents/Resources/AppIcon.icns"
echo "When this file exists, app will not store configs under this folder" > "$PackagePath/YiLink.app/Contents/MacOS/NotStoreConfigHere.txt"
chmod +x "$PackagePath/YiLink.app/Contents/MacOS/YiLink"

cat >"$PackagePath/YiLink.app/Contents/Info.plist" <<-EOF
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
  <key>CFBundleDevelopmentRegion</key>
  <string>English</string>
  <key>CFBundleDisplayName</key>
  <string>YiLink</string>
  <key>CFBundleExecutable</key>
  <string>YiLink</string>
  <key>CFBundleIconFile</key>
  <string>AppIcon</string>
  <key>CFBundleIconName</key>
  <string>AppIcon</string>
  <key>CFBundleIdentifier</key>
  <string>2dust.YiLink</string>
  <key>CFBundleName</key>
  <string>YiLink</string>
  <key>CFBundlePackageType</key>
  <string>APPL</string>
  <key>CFBundleShortVersionString</key>
  <string>${Version}</string>
  <key>CSResourcesFileMapped</key>
  <true/>
  <key>NSHighResolutionCapable</key>
  <true/>
</dict>
</plist>
EOF

create-dmg \
    --volname "YiLink Installer" \
    --window-size 700 420 \
    --icon-size 100 \
    --icon "YiLink.app" 160 185 \
    --hide-extension "YiLink.app" \
    --app-drop-link 500 185 \
    "YiLink-${Arch}.dmg" \
    "$PackagePath/YiLink.app"