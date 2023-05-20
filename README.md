# ResoName（old name：AddImageResolutionToFileName）
## これは何？
このソフトは、画像ファイルのファイル名にその画像の解像度（幅と高さ）を追加するだけのプログラムです。  
ある日、壁紙配布サイトから大量の壁紙をダウンロードしたところ、それぞれの解像度がバラバラでした。  
ファイルのプロパティを確認するのも時間がかかり、エクスプローラーでホバー表示するのも若干のラグが発生します。  
しかし、ファイル名に解像度が含まれていると、選別が容易になりますが、手動でやるのは面倒。そこで、このソフトを作成しました。

## どうやって使うの？
このソフトウェアを起動したら、画像ファイルをウィンドウにドラッグアンドドロップするだけで、簡単に変更可能。  
複数の画像ファイルと、フォルダーのドラッグアンドドロップに対応。  
フォルダーの場合はそのフォルダー以下のすべてのファイルをリネームできます。

対応フォーマットは  
・PNG  
・JPEG  
・ICON  
・BMP  
・TIFF  
・GIF  
・WebP  

マニュアルモードに切り替えると、幅と高さを手動で指定可能。  
ポジションで解像度を付ける位置を指定。  
ファイル名の先頭に追加するか、末尾に追加するかを選択可能。

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
ソフトの状態はsetting.xmlを同一ディレクトリに自動生成して保存するので、適当なフォルダーに入れてショートカットを作成した方が使いやすいと思います。  

## アンインストール方法
任意の場所に設置したソフトを、通常のファイル削除と同様の手順で削除する。  
setting.xmlやショートカットがあ



## English Ver
## What is this?
This software is a program that simply adds the resolution (width and height) of an image to the file name of the image file.  
One day, when I downloaded a large number of wallpapers from a wallpaper distribution site, each wallpaper had a different resolution.  
Checking the properties of each file takes time, and there is also a slight lag when hovering over them in Explorer.  
However, if the resolution is included in the file name, it becomes easier to sort them. But doing it manually is a hassle.  
So, I created this software.

## How use it?
Once you launch this software, you can easily make changes by simply dragging and dropping image files onto the window.  
This software allows you to drag and drop multiple image files as well as folders.  
In the case of folders, you can rename all files within that folder and its subfolders.

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
Just download, extract, and place it in any location.  
The software's status is automatically generated and saved in "setting.xml" in the same directory, so it is recommended to put it in a suitable folder and create a shortcut for ease of use.

## Uninstall Method
To delete the software that has been placed in any location, follow the same procedure as a normal file deletion.  
If there is a "setting.xml" file or a shortcut, delete them in the same way.


## issue
31680×17280の画像でリネームできない問題を発見  
近い解像度の30720×17280なら問題ない  
デバッグすると解像度をうまく取得できておらず、直後に「System.IO.IOException: The process cannot access the file because it is being used by another process.」が発生していることを確認。大きすぎて現行方式では取得できないか、記述にミスがある可能性あり。

## Version
- Ver.0.1.0 earlier are beta versions
- 0.1.0 2023/05/14(UTC+09) : First release
- 0.1.1 2023/05/21(UTC+09) : File can be renamed with "Open in File"