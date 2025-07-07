#!/bin/bash

Arch="$1"
OutputPath="$2"
Version="$3"

FileName="YiLink-${Arch}.zip"
wget -nv -O $FileName "https://github.com/lingyicute/YiLink-Desktop-core-bin/raw/refs/heads/master/$FileName"
7z x $FileName
cp -rf YiLink-${Arch}/* $OutputPath

PackagePath="YiLink-Package-${Arch}"
mkdir -p "${PackagePath}/DEBIAN"
mkdir -p "${PackagePath}/opt"
cp -rf $OutputPath "${PackagePath}/opt/YiLink"
echo "When this file exists, app will not store configs under this folder" > "${PackagePath}/opt/YiLink/NotStoreConfigHere.txt"

if [ $Arch = "linux-64" ]; then
    Arch2="amd64" 
else
    Arch2="arm64"
fi
echo $Arch2

# basic
cat >"${PackagePath}/DEBIAN/control" <<-EOF
Package: YiLink
Version: $Version
Architecture: $Arch2
Maintainer: https://github.com/lingyicute/YiLink-Desktop
Description: A GUI client for Windows and Linux, support Xray core and sing-box-core and others
EOF

cat >"${PackagePath}/DEBIAN/postinst" <<-EOF
if [ ! -s /usr/share/applications/YiLink.desktop ]; then
    cat >/usr/share/applications/YiLink.desktop<<-END
[Desktop Entry]
Name=YiLink
Comment=A GUI client for Windows and Linux, support Xray core and sing-box-core and others
Exec=/opt/YiLink/YiLink
Icon=/opt/YiLink/YiLink.png
Terminal=false
Type=Application
Categories=Network;Application;
END
fi

update-desktop-database
EOF

sudo chmod 0755 "${PackagePath}/DEBIAN/postinst"
sudo chmod 0755 "${PackagePath}/opt/YiLink/YiLink"
sudo chmod 0755 "${PackagePath}/opt/YiLink/AmazTool"

# desktop && PATH

sudo dpkg-deb -Zxz --build $PackagePath
sudo mv "${PackagePath}.deb" "YiLink-${Arch}.deb"