   M S B A M L   `   `   `  ????O  KМессенджер, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:МессенджерМессенджер  5   	       Мессенджер.Vhod   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:Мессенджер     5   	   _ [MaterialDesignThemes.Wpf, Version=4.6.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     ? WindowStartupLocation$  CenterScreen=?5	   	    ? Title$ 
MainWindow??5
   	   $	??800??6   $
??1200??6)   .??5      ? ???#b7ced2.+?5   
   ?? $	??500??6   $	??350??6!   $??120,0,120,0q?61   ???#dcdde2$??Center=?6[    ?? Padding$ 30q?6v   $	??550??6?    ?? CornerRadius$ 20??6?    J? Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 60Q?6#      Color$ #dcdde2??63      ShadowDepth$ 0Q?6C   ._?5      ?? .+?5      ? $??0 0 5 0q?6    ? ColumnDefinitions 5      ?? 5      $
??191* ?6+   ?? 5      $
??299* ?6+   .+?5      ?? $??Вход??6    $??30(?6,   $
??Bold"?6:   $??
10,10,0,20q?6L   ???	#FF6362506`   ?? 5      -   5        	 ??Name$	 btn_per_zareg??5      $
??Left=?64   $??Регистрация??6O      MaterialDesignFlatButton#??[  6e   $??1??6?   $??147,12,-3,16q?6?   $	??152??6?   
 ? Cursor$

 Hand??6?   $??18(?6?   
???   ????	#FF6362506  5       ?? 5!      $??Center=?6   $	??238??6;   .+?5"      ?? -   5         ??Name$ Name_txt??5"      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Введите логин??60    % !MaterialDesignFloatingHintTextBox#??[ 6_   $
 IBeam??6?   $??20(?6?   $??-40 0q?6?    ?? 
CaretBrush
 ?   ? ?? SelectionBrush
 ?   ?6?   G? 5$      -   5         G?Name$ Pasw_txt??5$   #   $! Введите пароль??65    ) %MaterialDesignFloatingHintPasswordBox#??[ 6e   $??20(?6?   $??-40,0q?6?    G? SelectionBrush
 ?   ? G? 
CaretBrush
 ?   ?6?   5&      ?? 5'      -   5        $	 btn_vhod_v_akkaynt??5'      $??
Войти??65   $??86,40,83,25q?6E   ???	#FF636250???	#FF636250$??15(?6?   $

 Hand??6?   6?   5(      5)      5*      5+      