#!/bin/bash

Arch="$1"
OutputPath="$2"

OutputArch="YiLink-${Arch}"
FileName="YiLink-${Arch}.zip"

wget -nv -O $FileName "https://github.com/lingyicute/YiLink-Desktop-core-bin/raw/refs/heads/master/$FileName"

ZipPath64="./$OutputArch"
mkdir $ZipPath64

cp -rf $OutputPath "$ZipPath64/$OutputArch"
7z a -tZip $FileName "$ZipPath64/$OutputArch" -mx1