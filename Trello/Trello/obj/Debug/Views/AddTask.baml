   M S B A M L   `   `   `  ����A  =Trello, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Trello.ViewModelsTrello.ViewModels  5   	       Trello.Views.AddTask   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Trello.ViewModels     5   	     � Title$  AddTask��5   	   $	��450��6   $	��800��6&    � 
ResizeMode$ NoResize=�62    � DataContext 5	      #   Trello.ViewModels.AddTaskView  5
   
   6   .��5      �  � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   .+�5   
   �� $��Cancel��6   $��40��6"   $��90��6.   $
��Left=�69   $��Bottom=�6T   $��	10,0,0,10q�6o   6�   �� 5   
   $��Enter��6   $��5��6;   $��40��6K   $��90��6W   $��Right=�6b   $��Bottom=�6~   $��	0,0,10,10q�6�   ��6!   �� *enter+6�   �� 5   
   $
��Name��6   $��0��6   $��Center=�6/   $	��Top=�6L   $��40(�6d   $��Rockwell)�6r   6�   �� 5   
   $��Description��6   $��1��6&   $��Center=�66   $��Stretch=�6S   $��20(�6o   $��Rockwell)�6}   6�   �� 5   
   $��Due Date(MM/DD/yyyy)��6   $��2��6/   $��Center=�6?   $��Stretch=�6\   $��10(�6x   $��Rockwell)�6�   6�   �� 5   
   $��Priority��6   $��3��6#   $��Center=�63   $��Stretch=�6P   $��20(�6l   $��Rockwell)�6z   6�   �� 5   
   $	��TDL��6   $��4��6   $��Center=�6.   $��Stretch=�6K   $��20(�6g   $��Rockwell)�6u   6�   �� 5   
   $��Category��6   $��5��6#   $��Center=�63   $	��Top=�6P   $��20(�6h   $��Rockwell)�6v   6�   �� 5   
   $��Center=�6,   $	��Top=�6I   $��20(�6a   $��Rockwell)�6o   $	��113��6�   $��61��6�   $��	0,173,0,0q�6�   ��6   �� *
taskName+6�   �� 5   
   $��1��6/   $��Center=�6?   $	��Top=�6\   $��20(�6t   $��Rockwell)�6�   $	��113��6�   $��61��6�   $��	0,173,0,0q�6�   ��6   �� *description+6�   �� 5    
   $��2��6,   $��Center=�6<   $	��Top=�6Y   $��20(�6q   $��Rockwell)�6   $	��113��6�   $��61��6�   $��	0,173,0,0q�6�   ��6   �� *
deadline+6�   �� 5!   
   $��3��6Y   $��Center=�6i   $	��Top=�6�   $��20(�6�   $��Rockwell)�6�   $	��113��6�   $��61��6�   $��	0,173,0,0q�6�   ��6   �� *
priorities+ �� SelectedValue 66   �� *
priority+6�   �� 5"   
   $��4��6,   $��Center=�6<   $	��Top=�6Y   $��20(�6q   $��Rockwell)�6   $	��113��6�   $��61��6�   $��	0,173,0,0q�6�   ��6   �� *
rootName+6�   �� 5#   
   $��5��6Y   $��Center=�6i   $	��Top=�6�   $��12(�6�   $��Rockwell)�6�   $	��113��6�   $��61��6�   $��	0,173,0,0q�6�   ��6   �� *
categories+ 66   �� *
category+6�   5%      5'      