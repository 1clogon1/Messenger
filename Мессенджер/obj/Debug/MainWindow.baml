   M S B A M L   `   `   `  ????O  KМессенджер, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:МессенджерМессенджер  5   	   %    Мессенджер.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:Мессенджер     5   	   _ [MaterialDesignThemes.Wpf, Version=4.6.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     ? WindowStartupLocation$  CenterScreen=?5	   	    ? Title$ 
MainWindow??5
   	   $	??800??6   $
??1200??6)   .??5      ? ???#b7ced2.+?5   
   ?? $	??500??6   $	??400??6!   $??120,0,120,0q?61   ???#dcdde2$??Center=?6[    ?? Padding$ 30q?6v   $	??550??6?    ?? CornerRadius$ 20??6?    J? Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 60Q?6#      Color$ #dcdde2??63      ShadowDepth$ 0Q?6C   ._?5      ?? .+?5      ? $??0 0 5 20q?6    ? ColumnDefinitions 5      ?? 5      $
??191* ?6+   ?? 5      $
??299* ?6+   .+?5      ?? $??Регистрация??6    $??30(?63   $
??Bold"?6A   $??	0,10,0,20q?6S   ???	#FF6362506f   ?? 5       -   5        	 ??Name$	 btn_per_Vhod??5       $
??Left=?63   $??
Войти??6N      MaterialDesignFlatButton#??[  6^   $??1??6?   $??198,18,0,20q?6?   $??88??6?   
 ? Cursor$

 Hand??6?   $??18(?6?   
??????????	#FF636250???	#FF6362506  5!      ?? 5"      $??Center=?6   $	??238??6;   .+?5#      ?? -   5         ??Name$ 	Login_txt??5#      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Введите логин??61    % !MaterialDesignFloatingHintTextBox#??[ 6`   $
 IBeam??6?   $??13(?6?    ?? 
CaretBrush
 ?   ? ?? SelectionBrush
 ?   ????	#FF6362506?   G? 5%      -   5         G?Name$ Pass_txt??5%   "   $! Введите пароль??64    ) %MaterialDesignFloatingHintPasswordBox#??[ 6d   $??13(?6?    G? SelectionBrush
 ?   ? G? 
CaretBrush
 ?   ????	#FF6362506?   G? 5&      -   5        $ 	Pass2_txt??5&   "   $' !Повторите парольн??65   #??[ 6h    G? Password$  ??6?   $??13(?6?   
 ?   ?
 ?   ????	#FF6362506?   ?? 5'      -   5        $ 	Email_txt??5'      $ Email??61   #??[ 6X   $??13(?6?   
 ?   ?
 ?   ????	#FF6362506?   5)      ?? 5*      -   5        $	 btn_zaregestrir_akkayn??5*      $*??$Зарегистрироваться??69   $
??0 20q?6V   ???	#FF636250???	#FF636250$	??276??6?   $??15(?6?   $

 Hand??6?   6?   5+      5,      5-      5.      