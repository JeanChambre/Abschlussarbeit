   M S B A M L   `   `   `  ����E  ABanksystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Banksystem
Banksystem  5          Banksystem.Kontostand   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:Banksystem     5      .��5	      �   � RowDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $��30 �6   �� 5      $��30 �6    � ColumnDefinitions 5   
   �� 5      $��* �6   6+   �� 5      $��* �6   6+   �� 5      $��* �6   6+   �� 5      $��* �6   6+   .+�5   
   �� $��Konto auswählen:��6   $��20,0,0,0q�6+   6?   �� 5   
   -   5        $��
Kontoliste��5   #   $��1��6   6f   �� 5   
   $��Aktuelles Konto��6   $��1��6*   $��20,0,0,0q�67   6K   �� 5   
   -   5         ��Name$ aktuellesKonto��5      $��1��6(   $��1��65   6G   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ AlleTransaktion��5   (   
����·�   AlternatingRowBackground
 ��·�   AutoGenerateColumns .  $��2��6�   $��4��6�      CanUserAddRows .     
IsReadOnly . $��	20,0,20,0q�6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 TransaktionID��6%   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6<   �� *TransaktionID+6`     5         Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6%   $	 	Kommentar��6/   
 6B   �� *	Comment+6`     5      $	 Datum��6%   
 64   �� *Date+ �� StringFormat$ 
dd.MM.yyyy��6h     5       $	 Betrag��6%   
 65   �� *Amount+$ {0}€��6g   5"      � 5#   
   $��3��6   $��3��6    5$      �� 5%      $��* �6#   6/   �� 5&      $��* �6#   6/   .+�5,      �� -   5        $��Zurück��5,      $��5��6;   $��0,0,20,0q�6K   6`   5-      5.      5/      