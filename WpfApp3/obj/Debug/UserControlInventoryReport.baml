   M S B A M L   `   `   `  ����B  >WpfApp3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp3WpfApp3  5      (    "WpfApp3.UserControlInventoryReport   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:WpfApp3     5      $	��585��5      $	��697��6+   
�������.��5      � -   5          �Name$  	chartGrid��5      $
��0,10q�6   $��3��6,   
������� � RowDefinitions 5	   
   �� 5
      $��30 �6   5      �� 5      5      .+�5   
   �� -   5         ��Name$ 
showReport��5      $��0��6%   $��Inventory Report��62   $��Right=�6M   $��0,4,4,4q�6�   $	��102��6�   �� 5   
   -   5        $ Update��5      $��0��6!   $��Update��6.   $	��Top=�6?   $��Right=�6W   $��	0,4,111,0q�6s   $��88��6�   6�   �� 5   
   -   5        $ showReportSales��5      $��0��6*   $��Sales Stock Report��67   $��Right=�6T   $��	0,4,204,4q�6p   $	��119��6�   �� 5   
   -   5        $ ChartViewSales��5      $��
Chart View��6)   $��0��6>   $��Right=�6K   $��	0,4,328,4q�6g   $��77��6z   6�   � 5   
   -   5        $  
ChartGrid2��5      $��1��6#   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ ReportViewSales��5      $��1��60   ���	#FFDBDBDB6T   5      5      5      