# CapsLockIndicator
Indicator CapsLock status 
大写锁定键指示器

环境
Windows 10 x64
.NET 5.0

![image](https://user-images.githubusercontent.com/41336794/184457803-38b4eb4b-8b91-4ee7-af0f-e694be57ae7d.png)
![image](https://user-images.githubusercontent.com/41336794/184457879-d595cce3-5273-4233-ac38-ea55d9755359.png)

指示CapsLock按键的状态

另外还增加了一点使用的功能：

1.鼠标左键点击，就会将界面切换到桌面

2.有关闭代理服务器的功能，这个是为了解决ShadowsocksR未正常关闭就重启电脑时，设置->网络和Internet->代理->手动设置代理会保持开启状态，导致系统没有网络连接

3.有隐身功能，防止指示图标挡住桌面上的内容

4.还有锁定和关机功能

注意，exe文件需要和dll文件在同一目录下，才能正常运行，你可以把手动它加入到系统启动路径中

程序是通过GetKeyState函数，每200毫秒轮询CapsLock键状态，判断大小写的，这种方法可能会占用系统的一丢丢资源，但是经过我的实测，没有太大影响
