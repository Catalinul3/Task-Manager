   M S B A M L   `   `   `  ����A  =Trello, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Trello.ViewModelsTrello.ViewModels  5   	       Trello.Views.AddSubRoots   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Trello.ViewModels     5   	     � Title$  AddSubRoots��5	   	   $	��450��6   $	��800��6*    � 
ResizeMode$ NoResize=�66    � DataContext 5
      '   !Trello.ViewModels.AddSubRootsView  5   
   6"   .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   6+   �� 5      $��* �6   6+    � RowDefinitions 5   
   �� 5      $��* �6   6)   �� 5      $��* �6   6)   .+�5   
   � $��0��6   $��0��6   $��2��6,   .+�5      �� $��	Type Root��6   $��20(�6)    �� TextAlignment$ Center=�67   6P   �� 5      $��0��60   $��0��6=   $	��Top=�6M   $��300,50,300,20q�6e   ��6   �� *
rootName+6e   �� 5      $��20(�6   $ Center=�6&   $��Center=�6=   �� 5      $�Add subRoot to��6   6.   5      �� 5      $��0��63   $��0��6@   $��295,127,295,30q�6P   ��6   �� *subrootName+6P   5      � 5   
   $��1��6   $��0��6   $��2��6,   .+�5!      �� $��	Add image��6   $ Center=�6)   $��
0,44,0,-44q�6@   6X   �� 5"      $��80��6   $��90��6    $��Uniform=�6h   ��6+   �� *image+ �� UpdateSourceTrigger$ PropertyChanged=�6|   �� 5#      $��<<��6   $��40��6(   $��20��63   $
��Left=�6?   $��Bottom=�6Z    J� RenderTransformOrigin$ 8.291,-2.561&�6u   $��
280,0,0,88q�6�   6�   �� 5$      $��<<��6   $��40��6k   $��20��6v   $
��Left=�6�   $��Bottom=�6�   $ 8.291,-2.561&�6�   $��
280,0,0,88q�6�   ��6(   �� *
previous+$ PropertyChanged=�6�   �� 5%      $��>>��6   $��40��6_   $��20��6j   $
��Left=�6v   $��Bottom=�6�   $ 8.291,-2.561&�6�   $��
475,0,0,88q�6�   ��6"   �� *next+$ PropertyChanged=�6�   �� 5&      $��Enter��6   $	��100��6?   $��60��6K   $��Bottom=�6W   $
��Left=�6r   ��6%   �� *enter+6�   �� 5'      $��Cancel��6   $	��100��6A   $��60��6M   $��Bottom=�6Y   $��Right=�6t   ��6&   �� *cancel+6�   5(      5)      5*      