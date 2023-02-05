# AddImageResolutionToFileName
## これは何？
これは画像ファイルのファイル名に、その画像の解像度（幅と高さ）を追加するだけのソフト。  
ある日壁紙配布サイトで大量の壁紙をダウンロードしたときに、それぞれ解像度がバラバラだった。  
ファイルのプロパティを見るのも時間がかかるし、エクスプローラーでホバー表示するのも若干のラグが発生する。  
ファイル名に解像度が載っていたら選別に便利じゃね？でも、手動でやるのは億劫だよ。そんな時に作ったもの。

## どうやって使うの？
起動したら画像ファイルをウィンドウにドラッグアンドドロップするだけ。  
画像のドラッグアンドドロップは単一ファイルだけではなく、複数ファイルにも対応。  
さらにフォルダーを使うと、階層以下の対象のファイルすべてをリネーム可能。  
対応フォーマットは  
・PNG  
・JPEG  
・ICON  
・BMP  
・TIFF  
・GIF  
・WebP  

マニュアルモードにすると手動で幅と高さを指定可能。  
ポジションは解像度をつける位置。ファイル名の先頭に追加するか、末尾に追加するかを決められる。  

結合記号について（いい名前が思いつかなかった）  
ファイル名の例：image_128×128.png  
・ファイル名は、解像度とファイル名の間につける記号  
例えば上の例の場合だと「 _ 」の部分  
・解像度は、幅と高さの間につける記号  
例えば上の例の場合だと「 × 」の部分  

各入力エリアの制約はファイル名に使用不可能な  「 \ / : * ? \ " > < | 」の9個だけ。  
それ以外の制約はない。  

## インストール方法
ダウンロードして、展開して、任意の場所に設置するだけ。
ソフトの状態はSetting.xmlを同一ディレクトリに自動生成して保存するので、適当なフォルダーに入れてショートカットを作成した方が使いやすいと思う。  

## アンインストール方法
任意の場所に設置したソフトを、通常のファイル削除と同様の手順で削除する。  
Setting.xmlやショートカットがある場合は、同様に削除する。  


## English Ver
## What is this?
This is a software that only adds the resolution (width and height) of an image to the file name of the image file.  
One day, when downloading a large number of wallpapers from a wallpaper distribution site, the resolutions of each were different.  
It takes time to look at the file's properties and there is a slight lag when hover with Explorer.  
It would be convenient if the resolution was included in the file name when sorting, but it's a hassle to do it manually. That's what was made for that time.

## How use it?
Just drag and drop the image files into the window after starting it.  
The drag and drop of the images is not just for single files, but also for multiple files.   
Additionally, using folders allows you to rename all of the target files below the hierarchy.  
Supported image formats  
・PNG  
・JPEG  
・ICON  
・BMP  
・TIFF  
・GIF  
・WebP  

In manual mode, you can manually specify the width and height.  
The position is the location to add the resolution.  
You can determine whether to add it to the beginning of the file name or the end.  

Regarding the combination symbol (couldn't think of a good name)  
File name example: image_128x128.png  
-The symbol between the resolution and the file name.  
For example, in the above example, the part of "_"  
-The symbol between the width and height of the resolution.  
For example, in the above example, the part of "x"  

The only constraints on each input area are the 9 characters that cannot be used in filenames:  / : * ? " > < | .  
There are no other constraints.  

## Installation Method
Just download, extract, and place it anywhere.   
The software state is saved by automatically generating and saving the Setting.xml in the same directory, so it's probably easier to put it in an appropriate folder and create a shortcut.  

## Uninstall Method
Delete the software placed anywhere in the same manner as normal file deletion.  
If there is a Setting.xml or shortcut, delete it as well.  
