   M S B A M L   `   `   `  ����A  =Trello, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Trello.TDL
Trello.TDL  5   	   5clr-namespace:Trello.ViewModelsTrello.ViewModels  5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5   
   3clr-namespace:Trello.ConverterTrello.Converter  5	          Trello.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Trello.TDL     5   	   6
behavioursclr-namespace:Trello.ViewModels     5   	   ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5   
   4	converterclr-namespace:Trello.Converter     5	        � Title$  ToDoList��5   	   $	��450��6   $	��800��6'    � DataContext 5         Trello.TDL.TreeViewMVVM  5   
   .��5   
   �  � ColumnDefinitions 5   
   �� 5      $��* �6   6+   �� 5      $	��70* �6   6-   �� 5      $��* �6   6+    � RowDefinitions 5   
   �� 5      $��2* �6   6*   �� 5      $	��30* �6   6+   �� 5      $��0* �6   6*   �� 5      $��* �6   6)   .+�5   
   �� $��0��6   $��0��6$   $��3��61   
�������6U   �� 5    
   $��3��6   $��0��6$   $��3��61   
�������6U   �� 5!   
   $��0��6   $��0��6!   $��3��61   
�������6X   �� 5"   
   $��3��6   $��0��6!   $��3��61   
�������6X   � 5#   
   $��1��6   $��1��6    5$      �� 5%      $��* �6#   6/   �� 5&      $
��2.5* �6#   62   .+�5)      �  5*      �� 5+      $��* �6$   61   �� 5,      $��0.35* �6$   65   .+�5.      � $��1��6    5/      �� 50      $��* �6(   65   �� 51      $��7* �6(   66   .+�53      � $��1��6    � ShowGridLines .  54      �� 55      $��2* �6,   6:   �� 56      $��* �6,   69   .+�58      �  59      �� 5:   "   $��* �60   6=   �� 5;   "   $��* �60   6=   �� 5<   "   $��* �60   6=   .+�5>      �� 
�������6(   �� 5?      $��1��6(   
�������65   �� 5@      $��2��6(   
�������65   �� 5A      
����  ��� 5B   "   $�Text due today:��6&   6?    �� 5C   "   �6&   �� *	todayTask+6C   5D      �� 5E      $��1��6(   
����  ��� 5F   "   $�Text due tomorrow:��6&   6B    �� 5G   "   �6&   �� *tomorrowTask+6F   5H      �� 5I      $��2��6(   
����  ��� 5J   "   $�Task overdue:��6&   6=    �� 5K   "   �6&   �� *sizeOverdueTask+6I   5L      5M      � 5N      $��1��6    5O      �� 5P   "   $��* �60   6=   �� 5Q   "   $��* �60   6=   .+�5S      �� 
�������6(   �� 5T      $��1��6(   
�������65   �� 5U      
����  ��� 5V   "   $�Done tasks:��6&   6<    �� 5W   "   �6&   �� *
sizeDone+6B   5X      �� 5Y      
����  �$��1��6:   �� 5Z   "   $�Tasks to be done:��6&   6A    �� 5[   "   �6&   �� *
sizeUnDone+6D   5\      5]      5`      �� 5a      $��0��6    
�������6-   5b      R� 5c      -   5         R�Name$ toDoList��5c      ��6-   �� *itemsCollection+ �� UpdateSourceTrigger$ PropertyChanged=�.  (System.Windows.Interactivity.Interaction   	Behaviors 5d      4   .Trello.ViewModels.BindableSelectedItemBehavior  5e         SelectedItem 6B   �� *SelectedItem+	 �� Mode$	 TwoWay=�$ PropertyChanged=���5i      �� 5j      
 �� ItemsSource
 63   �� *tDLs+.R�5k      ��  �� Orientation$ 
Horizontal=�6)   .+�5l   "   �� $��20��6D   $��20��6O   ��6(   �� *
imageURL+6O   �� 5m   "   $��10(�6B   ��6,   �� *name+6B   5n      5p      5u      5v      � 5w      $��1��6    5x      �� 5y      $
��0.2* �6$   64   �� 5z      $
��1.6* �6$   64   �� 5{      $
��0.2* �6$   64   �� 5|      $��2* �6$   62    5~      �� 5      $��* �6'   63   �� 5�      $	��50* �6'   65   .+�5�      �� $��0��6   $��0��6)   $��2��69   
�������6M   �� 5�      $��2��6   $��0��6)   $��2��69   
�������6M   �� 5�      $��0��6   $��0��6,   $��4��69   
�������6J   �� 5�      $��0��6   $��0��6*    �� TextAlignment$ Center=�6:   $��2��6Q   �� 5�      $�Viewing��6    �� 5�      �6   �� *SelectedItem.name+ �� 5�      $�to-do list.��6    �� 5�      �6   �� *SelectedItem.size+ �� 5�      $�tasks show-down��6   5�      �� 5�      -   5         ��Name$ viewTask��5�      $+��%Selected task's notes and attachments��6.   $��2��6\   $��0��6i   $��2��6y   $ Center=�6�   �� 5�      $��1��6   $��1��6+   ��68   �� *SelectedItem.tasks+$ PropertyChanged=� �� SelectedItem 6�   �� *selectedTask+��5�      �� 5�       �� 
TargetType" ��6     �� Triggers 5�      �� 5�   "    �� Binding 6.   �� *isDone+ �� Value	 False6I   .a�5�   &   ��  �� Property � �� �� Value
 �����6C   5�   #   �� 5�   "    6.   �� *isDone+ True6I   .a�5�   &   ��  � ��
 � � �6C   5�   #   5�       �� View 5�      �� 5�      .P�5�      �� $��	Task Name��6-    �� Width$	 200��6@    �� DisplayMemberBinding 6L   �� *name+�� 5�      $��Priority��6-   $	 100��6?    6K   �� *
priority+6x   �� 5�      $
��Type��6-   $	 150��6;    6G   �� *status+�� 5�      $��Due Date��6-   $ 95��6?    6J   �� *
deadline+5�      5�      �� 5�      $��1��6   $
��Left=�6*   $��3��6E   $	��Top=�6}   ��6R   �� *selectedTask.description+6}   5�      5�      ]� 5�   
   $��0��6   $��0��6$   $��3��61   .+�5�      o� .@�5�      m� $
��File��6   .@�5�      m� $��Open Database��6   68   m� 5�      $��New Database��6   67   m� 5�      $��Archive Database��6   6;   m� 5�      $
��Exit��6   6/   5�      m� 5�      $	��TDL��6   .@�5�      m� $��Add Root TDL��6    m� Command 65   �� *	addRoot+$ PropertyChanged=�6x   m� 5�      $��Add Sub-TDL��6    64   �� *
addsubRoot+$ PropertyChanged=�6{   m� 5�      $
��Edit��6    6-   �� *edit+$ PropertyChanged=�6n   m� 5�      $��Delete��6    6/   �� *delete+$ PropertyChanged=�6r   m� 5�      $��Move Up��6    60   �� *moveUP+$ PropertyChanged=�6s   m� 5�      $��	Move Down��6    62   �� *
moveDown+$ PropertyChanged=�6w   m� 5�      $��Change Path��6   66   5�      m� 5�      $
��Task��6   .@�5�      m� $	��Add��6    6,   �� *	addTask+$ PropertyChanged=�6o   m� 5�      $
��Edit��6    6-   �� *
editTask+$ PropertyChanged=�6r   m� 5�      $��Delete��6    6/   �� *
deleteTask+$ PropertyChanged=�6u   m� 5�      $��Set Done��6    61   �� *	setDone+$ PropertyChanged=�6u   m� 5�      $��Move Up��6    60   �� *
moveUPTask+$ PropertyChanged=�6w   m� 5�      $��	Move Down��6    62   �� *moveDownTask+$ PropertyChanged=�6z   m� 5�      $��	Find Task��6   64   5�      m� 5�      $
��View��6   .@�5�      m� $
��Sort��6   .@�5�      m� $��By priority��6#    68   �� *sortByPriority+$ PropertyChanged=�6�   m� 5�      $��By deadline��6#    68   �� *sortByDeadline+$ PropertyChanged=�6�   5�      m� 5�      $��Filter��6   .@�5�      m� $��	Done task��6'    6:   �� *filterByDone+$ PropertyChanged=�6�   m� 5�      $��Undone task��6'    6<   �� *filterByUndone+$ PropertyChanged=�6�   m� 5�      $��Undone and late��6#    6<   �� *filterByUnDoneAndLate+$ PropertyChanged=�6�   m� 5�      $$��Undone task but no late status��6#   6M   m� 5�      $*��$Undone task but with nearly deadline��6'    6U   �� *!filterByUnDoneButNearlyDeadline+$ PropertyChanged=�6�   5�      5�      m� 5�      $
��Help��6   .@�5�      m� $��About��6   60   5�      5�      5�      5�      5�      