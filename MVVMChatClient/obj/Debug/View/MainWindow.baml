   M S B A M L   `   `   `  ����I  EMVVMChatClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:MVVMChatClientMVVMChatClient  5   	   N JMVVMChatClient.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulljHclr-namespace:MVVMChatClient.Core.ViewModel;assembly=MVVMChatClient.CoreMVVMChatClient.Core.ViewModel 5   	       MVVMChatClient.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:MVVMChatClient     5   	   YCoreHclr-namespace:MVVMChatClient.Core.ViewModel;assembly=MVVMChatClient.Core     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   Bz:http://schemas.microsoft.com/expression/2010/interactivity  5	   	   h dMicrosoft.Expression.Interactions, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions  5
   	   -   5          �Name$	  win��5
   N    � Title$ 
MainWindow��5   	   $	��750��6   $
��1000��6)    � DataContext4  .MVVMChatClient.Core.ViewModel.WindowsViewModel"  66    � WindowStyle$
 None=�6c    � 	Resources 5       �� DataType2  ,MVVMChatClient.Core.ViewModel.LogInViewModel �� ImageSource   MVVMChatClient.Login3  -MVVMChatClient.Core.ViewModel.SignInViewModel   MVVMChatClient.SignIn1  +MVVMChatClient.Core.ViewModel.ChatViewModel   MVVMChatClient.Chat��%�  (��       5      *, +)(�� �     5      *, +)(�� f    5%      *, +)�� 5   
   "  6   .R�5      �� ��5      �� 5      $( "Images/background-typography-4.jpg��6!   ._�5        $	��350��6   $	��400��6+   5      5      �� 5   
   "  6   .R�5      �� ��5      �� 5      $( "Images/background-typography-4.jpg��6!   ._�5!        $	��400��6   $	��400��6,   5"      5#      �� 5%   
   "  6   .R�5&        5'      5)      .  (System.Windows.Interactivity.Interaction   Triggers 5+      /	  )System.Windows.Interactivity.EventTrigger	  5,   
    	  	EventName$ Closing��6   .
  (System.Windows.Interactivity.TriggerBase	 
  Actions.	 5-      >  8Microsoft.Expression.Interactivity.Core.CallMethodAction  
   
MethodName$
 
Disconnect��6G      TargetObject 6"   �� *CurrentView+6G   5.      ��51      �� 52   
   $F @Images/figure_colorful_bright_background_blur_46937_1280x720.jpg��6   .��55      �  � RowDefinitions 56   
   �� 57      $��30 �6   �� 58      $��* �6   .+�5;   
   � -   5        ���	#FF233A3A � ColumnDefinitions 5<      �� 5=      $��* �6#   �� 5>      $��30 �6#   �� 5?      $��30 �6#   �� 5@      $��30 �6#   .+�5C      �� -   5         ��Name$ Minimize��5C      $��1��5D      $
��🗕��5E      
������ $��0q�5G      ���	#FFA61D1D$��20(�5I      $��30��5J      $��30��6    5K      �� 5M      -   5        $ Maximize��5M      $��2��5N      $
��🗖��5O      
������ $��0q�5Q      ���	#FFA61D1D$��20(�5S      $��30��5T      $��30��6    5U      �� 5W      -   5        $ Cloase��5X      $��3��5W      $	��✘��5Y      
������ $��0q�5[      ���	#FFA61D1D$��20(�5]      $��30��5^      $��30��6    5_      5a       � 5c   
   $��1��6     � NavigationUIVisibility$ Hidden=�6=   ��6   �� *CurrentView+6=   5d      5f      