   M S B A M L   `   `   `  ����   MVVMChatClient'    !MVVMChatClient.MessageItemControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       � 	Resources     OnMouseEnter1 �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value  OnMouseLeave1��%�   &	        &	 u     �� .��U� $ (FrameworkElement.Width)�$ border1��.\�  $ 0:0:0.5��$ 75Q�  $ 0:0:0.6��$ 75Q��� .��U� $ (FrameworkElement.Width)�$ border1��.\�  $ 0��$ 75Q�  $ 0:0:1��$ 50Q� � Triggers :�  :� RoutedEvent$ UIElement.MouseEnter��.[��� #��[  :� $ UIElement.MouseLeave��.[��� -    ��Name$# OnMouseLeave1_BeginStoryboard��#��[ .��� $
��Auto��$��4,0,10,0q����� *MessageAlignment+ � RowDefinitions �� $��* ��� $��* ��� $��* �	 � ColumnDefinitions	 �� $��* ��� $��* �.+��� -   
 ��Name$
 border1��$��	10 15 5 0q�$	��Top=�$
��Left=�$��50��
�������$��1q�$��3�� J� RenderTransformOrigin$ 0.5,0.5&�y��� *MessagePicture+ �� CornerRadius �� *ActualWidth+ �� RelativeSource �� *Self+���� *ActualWidth+ �� *Self+����  �� ImageSource �� *MessageSenderGender+ J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Opacity$	 0.3Q�   
BlurRadius$ 4Q�   	Direction$	 270Q��� $��5 0 0 5q� �� Padding$ 0, 10,10, 0q�$��14(����	#FFE0DBDB$��1������ *Name+y��� *Sender+�� -   $
 border�� �� Padding$ 6,0,0,6q�$ 8��
���   �$ 0.5,0.5&�$��1��$��1������ *MessageColour+   $	 0.3Q�$ 4Q�$	 270Q�._��� -    ��Name$ 
stackPanel��$��30��$
��Auto��$��1��$��1��.+��� -    ��Name$ 
textBlock1��$
��Wrap=�$	��250��$ 0 5 5 5q�$��20(�
���   �$��1��$��1������ *MessageText+�� -   $ 	textBlock��$ 5, 0, 0, 10q�$��14(����	#FFE0DBDB$ 0.5,0.5&�$��1��$��2������ *MessageTime+