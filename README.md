# NConstrictor [![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)
C#からPythonのC-APIを使い、メモリ操作、モジュールの関数呼び出しを行うライブラリです

![Sample](https://github.com/harujoh/NConstrictor/blob/Images/top.png)

## できること
- C#からPythonの値を”直接”変更
- C#からPythonの値を取得
- C#からPythonモジュールの呼び出し

## 動作原理
- C#からCPythonのC-APIを呼び出す
- CPythonからImportしたNumpyよりNumpyのC-APIを取得
- 取得した各APIを使用してC#からPython内のメモリ値を操作する

## メリット
- Pythonの全機能をC#から使用できる
- Pythonのライブラリも機能の制限を受けないためGPUによる処理の高速化が可能

## 動作環境
- Python3.7(PATHに追加されていること)
- Numpy1.15
