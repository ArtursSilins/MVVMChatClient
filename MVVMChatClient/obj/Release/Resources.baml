   M S B A M L   `   `   `  ���� I  EMVVMChatClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:MVVMChatClientMVVMChatClient  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   Button.Static.Background  �� Color  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Background  Button.Pressed.Border  Button.Disabled.Background  Button.Disabled.Border  Button.Disabled.Foreground 	 ButtonStyle1 �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� CornerRadius J� SnapsToDevicePixels ��Name	 �� Triggers
 P� Property P� Value �� 
TargetName 
 TextBox.Static.Border  TextBox.MouseOver.Border  TextBox.Focus.Border  TextBoxNameStyle-   'System.Windows.Input.KeyboardNavigation ��Name J� IsHitTestVisible
 � Tag �� Triggers �� Binding �� RelativeSource �� Value ]� 
Conditions �� Property �� Value  OptionMarkFocusVisual �� StrokeDashArray $  RadioButton.MouseOver.Background   RadioButton.MouseOver.Border  RadioButton.MouseOver.Glyph # RadioButton.Disabled.Background  RadioButton.Disabled.Border  RadioButton.Disabled.Glyph " RadioButton.Pressed.Background  RadioButton.Pressed.Border  RadioButton.Pressed.Glyph  RadioButton.Static.Glyph  RadioButtonStyle1  FocusVisual ! RadioButton.Static.Background  RadioButton.Static.Border �Name � ColumnDefinitions A�Name J� Opacity�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     &localclr-namespace:MVVMChatClient  %�  &	        &	      &	 *     &	 ?     &	 T     &	 i     &	 ~     &	 �     &	 �     &		 �     7   7  7  7  7  7  7  7  7  &	
 z    &	 �    &	 �    &	 �    7 
 7  7  &	 �    &	 �    &	     &	 )    &	 >    &	 S    &	 h    &	 }    &	 �    &	 �    &	 �    &	 �    7  7  7  7  7  7  7  7  7  7  7  7  7  7  �� $  	#FFDDDDDD���� $  	#FF707070���� $  	#FFBEE6FD���� $  	#FF3C7FB1���� $  	#FFC4E5F6���� $  	#FF2C628B���� $  	#FFF4F4F4���� $  	#FFADB2B5���� $  	#FF838383���� " ��.���  � ��8   ��  � ��8  ��  � ��# � ����  � ��$ 1q���  � ��$ Center=���  � ��$ Center=���  � ��$ 1q���  � �� �� " ��.R��� $ border��$ 15��#��z��#��z��#��z�� . ._��� $ contentPresenter��|�.  #��z��#��z����. # z #��z��	 P� 
 �@��IsDefaulted . . ��� $ border��# � �� � ��P� 
 �@J�IsMouseOver . . ��� $ border��8   � ���� $ border��8   � ��P� 
 � �� . . ��� $ border��8   � ���� $ border��8   � ��P� 
 � {� .  . ��� $ border��8   � ���� $ border��8   � ���� $ contentPresenter��8   � ���� $  	#FFABAdB3���� $  	#FF7EB4EA���� $  	#FF569DE5���� " ��.���  � ��# � ����  � ��8   ��  � ��# � ����  � ��$ 1q���  �@  TabNavigation$
 None=���  � ��$
 Left=���  �@�FocusVisualStyle Z� ��  �@J�	AllowDrop . ��  �@��PanningMode$ VerticalFirst=���  �@��IsFlicksEnabled .  ��  � �� �� " ��.R�� .+��� $ border��#��z��#��z��#��z�� . ._��� $ PART_ContentHost��|�.  $��Hidden=�$��Hidden=���  .  #��z $��15(�
�������$��Center=�$��Center=����� " ��.���  � y�$ 	Collapsed=� ��  �� *Text+ �� *TemplatedParent+  .a���  � y�$ Visible=�	 P� 
 � {� .  . ��� $ border�� �@J�Opacity$
 0.56Q�P� 
 �@J�IsMouseOver . . ��� $ border��8   � ��P� 
 �@J�IsKeyboardFocused . . ��� $ border��8   � �� ]�  �� + �@��#IsInactiveSelectionHighlightEnabled . ��  �@��IsSelectionActive .  ./���  �@��SelectionBrush# � /��� .���  � �� �� .R��� $��14,0,0,0q� . #��� ��$��1��$	 1 2R��� $  	#FFF3F9FF���� $  	#FF5593FF���� $  	#FF212121���� $  	#FFE6E6E6���� $  	#FFBCBCBC���� $  	#FF707070���� $  	#FFD9ECFF���� $  	#FF3C77DD���� $  	#FF212121���� $  	#FF212121���� " 	�.���  �@�FocusVisualStyle8   ��  � ��8  ��  � ��8  ��  � ��# � ����  � ��$ 1q���  � �� �� " 	�.R�� $ templateRoot��
������  .  �� $
��Auto ��� $��* �.+��� $ radioButtonBorder��#��z��#��z��#��z��$	 100��#��z��$��1,1,2,1q�#��z��$��20��$��20��._�� $ markGrid��$��2q�.+�A� $ 
optionMark��8�� $��6��$��6��$ 0Q��� $ contentPresenter��$��1��|�.  #��z��#��z����. # z #��z��	 P� 
 � �� . . ��� 8   �@�FocusVisualStyle��  � ��$ 4,-1,0,0q�P� 
 �@J�IsMouseOver . . ��� $ radioButtonBorder��8   � ���� $ radioButtonBorder��8   � ���� $ 
optionMark��8   � ��P� 
 � {� .  . ��� $ radioButtonBorder��8   � ���� $ radioButtonBorder��8 	  � ���� $ 
optionMark��8 
  � ��P� 
 � �� . . ��� $ radioButtonBorder��8   � ���� $ radioButtonBorder��8   � ���� $ 
optionMark��8   � ��P� 
 �@d�	IsChecked$
 trueY�. ��� $ 
optionMark�� �@J�Opacity$ 1Q�P�  Z� 
 �@d�	IsChecked. ��� $ 
optionMark�� �@J�Opacity$
 0.56Q�