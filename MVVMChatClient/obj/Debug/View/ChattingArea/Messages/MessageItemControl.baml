   M S B A M L   `   `   `  ����  MVVMChatClient'    !MVVMChatClient.MessageItemControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      h dMicrosoft.Expression.Interactions, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions  6S     � 	Resources  5	         OnMouseEnter1 �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value  OnMouseLeave1��%�  &	        &	 �     �� 5
   
   .��5      U� $ (FrameworkElement.Width)�6,   $ border1��6a   .\�5        $ 0:0:0.5��6'   $ 80Q�69     5      $ 0:0:0.6��6'   $ 80Q�69   5      5      �� 5   
   .��5      U� $ (FrameworkElement.Width)�6,   $ border1��6a   .\�5        $ 0��6'   $ 80Q�63     5      $ 0:0:1��6'   $ 50Q�67   5      5      5      .��5      � $
��Auto��6   $��4,0,10,0q�6   ��6,   �� *MessageAlignment+ � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5       $��* �6   .+�5#   
   �� -   5         ��Name$ border1��5#      $��	10 15 5 0q�5$      $	��Top=�5%      $
��Left=�5&      $��50��5'      
�������$��1q�5+      $��3��5,       J� RenderTransformOrigin$ 0.5,0.5&�6"   y�5#   "   �� *MessagePictureVisibility+	 �� CornerRadius	 5(      �� *ActualWidth+
 �� RelativeSource
 �� *Self+��5)      �� *ActualWidth+
 �� *Self+ � Triggers 5.      :� 5/       :� RoutedEvent$ UIElement.MouseEnter��6   .[�50      �� #��[  6&   51      :� 53      $ UIElement.MouseLeave��6   .[�54      �� -   5         ��Name$# OnMouseLeave1_BeginStoryboard��54   &   #��[ 55      56      ��59      �� 5:       j� Stretch$ UniformToFill=�6D    �� ImageSource 6   �� *MessagePicture+6D    J� Effect 5=      3  -System.Windows.Media.Effects.DropShadowEffect  5>         Opacity$	 0.3Q�6#      
BlurRadius$ 4Q�61      	Direction$	 270Q�6@   5A      �� 5C   
   $��5 0 0 5q�5E      $��14(�5F      ���	#FFE0DBDB$��1��5H      ��5C      �� *Name+y�5D      �� *Sender+5H      �� 5J   
   -   5        $ border��5J       �� Padding$ 6,0,0,6q�5K      $	 8��5L      
���   �$ 0.5,0.5&�5O      $��1��5P      $��1��5Q      ��5N      �� *MessageColour+ 5T        5U      $	 0.3Q�6#   $ 4Q�61   $	 270Q�6@   ._�5X      �� -   5         ��Name$ 
stackPanel��5X      $��30��5Y      $
��Auto��5Z      $��1��5[      $��1��5\      .+�5_      �� -   5         ��Name$ 
textBlock1��5_      $
��Wrap=�5`      $	��250��5a       �� Padding$ 0 5 5 5q�5c      $��20(�5d      
���   �$��1��5f      $��1��5g      ��5b      �� *MessageText+5g      5h      5i      �� 5j   
   -   5        $ 	textBlock��5j      $ 5, 0, 0, 10q�5l      $��14(�5m      ���	#FFE0DBDB$ 0.5,0.5&�5o      $��1��5p      $��2��5q      ��5k      �� *MessageTime+5q      5r      5u      