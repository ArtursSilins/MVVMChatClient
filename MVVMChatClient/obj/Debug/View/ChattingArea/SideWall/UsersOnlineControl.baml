   M S B A M L   `   `   `  ����  MVVMChatClient'    !MVVMChatClient.UsersOnlineControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $��70��5   7   $	��250��6C     � 	Resources  5	         OnMouseEnter1	 �� To g� Duration �� 
TargetName �� TargetProperty  OnMouseLeave1��%F  &	        &	 �     �� 5
   
   .��5      �� $ #5cb863X�6   $ 0:0:0.3D�6*   $ border1��6=   $1 +(Border.Background).(SolidColorBrush.Color)�6]   5      �� 5   
   .��5      �� $ 	#FFD8D8D8X�6   $ 0:0:0.3D�6,   $ border1��6?   $1 +(Border.Background).(SolidColorBrush.Color)�6_   5      5       � Triggers 5      :� 5   
    :� RoutedEvent$ 
MouseEnter��6   .[�5      �� #��[  6   5      :� 5   
   $ 
MouseLeave��6   .[�5      �� -   5         ��Name$# OnMouseLeave1_BeginStoryboard��5      #��[ 5      5      .��5      � $��0 2 0 2q�6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5       $��* �6   �� 5!      $��* �6   	 � ColumnDefinitions	 5#   
   �� 5$      $��* �6   �� 5%      $��* �6   .+�5)   
   �� -   5        
 ��Name$
 border2��5)      $��3��5*      $��2��5+      $��0 2 80 2q�5,      ���#5cb863._�50   
   �� -   5        $
 border1��50      $��3��51      $��2��52      $��0 6 0 6q�53      ���	#FFD8D8D8._�56      �� $
��Wrap=�6   $��Center=�57      $��SemiBold"�5:      
���   �$��1��5<      $	��160��5=      $��
0, 0, 0, 0q�5>       �� Padding$ 5 5 5 5q�5?      ��58      �� *
UserName+��59      �� *
FontSize+5A      5C      5D      A� 5F   
   $��	-10 0 0 0q�5G      $��65��5H      $��65��5I      $��1��5J      $��3��6"   $��0��5K      $��3��6    J� Effect 5L      3  -System.Windows.Media.Effects.DropShadowEffect  5M         Opacity$	 0.5Q�6#      
BlurRadius$ 10Q�61   ��5O      �� 5P       j� Stretch$ UniformToFill=�6A    �� ImageSource 6   �� *UserPicture+6A   5R      5S      5T      