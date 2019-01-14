# NConstrictor [![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)
C#とPython間で高速にデータ通信を行うライブラリです

![Sample](https://github.com/harujoh/NConstrictor/blob/Images/top.png)

## できること
- C#からPython内のNdArrayの値を”直接”変更する
- PythonにC#の配列または多次元配列を送信
- PythonのNdArrayをC#の配列または多次元配列として取得

## 動作原理
- C#からCPythonのC-APIを呼び出す
- CPythonからImportしたNumpyよりNumpyのC-APIを取得
- 取得した各APIを使用してC#からPython内のメモリを直接操作する

## メリット
- Pythonの全機能をC#を介して使用できる
- Pythonライブラリも機能の制限を受けないためGPU等による高速な処理を期待できる

## 動作環境
- Python3.7(PATHに追加されていること)
- C# 7.2
