   M S B A M L   `   `   `  ????O  KМессенджер, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:МессенджерМессенджер  5   	   $    Мессенджер.Perepuska   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:Мессенджер     5   	   _ [MaterialDesignThemes.Wpf, Version=4.6.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     ? WindowStartupLocation$  CenterScreen=?5	   	    ? Title$ 
MainWindow??5
   	   $	??800??6   $
??1200??6)   .??5      ? ???#b7ced2 ? RowDefinitions 5   
   ?? 5      $
??311* ?6   ?? 5      $
??208* ?6   ?? 5      $
??250* ?6    ? ColumnDefinitions 5   
   ?? 5      $
??273* ?6   ?? 5      $
??185* ?6   ?? 5      $
??138* ?6   .+?5   
   ?? $	??700??6   $	??500??6!   $??120,102,120,103q?61   ???#dcdde2$??Center=?6_    ?? Padding$ 30q?6z   $	??550??6?    ?? CornerRadius$ 20??6?   $??3??6?   $??3??6?    J? Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 60Q?6#      Color$ #dcdde2??63   	   ShadowDepth$	 0Q?6C   ._?5      ?? .+?5"      ? $??0 0 0 0q?6    5#      ?? 5$      $
??191* ?6+   ?? 5%      $
??299* ?6+   .+?5'      ?? -   5        
 ??Name$
 Name_Polzovat??5'       $??Сообщения??67   $??30(?6H   $
??Bold"?6V   $??26,10,135,20q?6h   $??2??6~   ???	#FF6362506?   ?? 5(      -   5         ??Name$ btn_vuhod_Vhod??5(      $
??Left=?66   $??Профиль??6Q      MaterialDesignFlatButton#??[  6c   $??1??6?   $??261,18,0,20q?6?   $	??113??6?    ? Cursor$
 Hand??6?   $??18(?6?   ???	#FF6362506?   ?? 5)      -   5        $ btn_vuhod_new_polzova??5)      $
??Left=?6=   $??Обновить??6X   #??[  6k   $??1??6?   $??113,19,0,19q?6?   $	??126??6?   $
 Hand??6?   $??18(?6?   ???	#FF6362506  5+      ?? 5,      -   5         ??Name$ scrollviewer??5,       $	??400??66   $	??300??6B   $??100,0,-60,100q?6O   .??5-      ?? -   5         ??Name$ MyPanel??5-   !   5.      ?? 53      $	??300??6   $??	0 -30 0 0q?6)   .+?54      ?? -   5         ??Name$ Perepuska_txt??54      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$' !Введите сообщение??65    % !MaterialDesignFloatingHintTextBox#??[ 6h   $ IBeam??6?   $??13(?6?    ?? 
CaretBrush
 ?   ? ?? SelectionBrush
 ?   ????	#FF6362506?   55      ?? 56      $??360 -55 0 0q?6   .+?57      ?? -   5        $ btn_otpravut_soobhenue??57      $
??Left=?6>   $??Отправить??6Y   #??[  6m   $??113,19,0,19q?6?   $	??134??6?   $
 Hand??6?   $??18(?6?   ???	#FF6362506?   58      5@      5A      5B      5C      