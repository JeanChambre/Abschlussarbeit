   M S B A M L   `   `   `  ����E  ABanksystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Banksystem
Banksystem  5          Banksystem.Hauptfenster   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:Banksystem     5      .��5	      �� .+�5
   
   �   � RowDefinitions  5      �� 5      $
��auto �6    60   �� 5      $��30 �6    6.   �� 5      $
��auto �6    60   �� 5      $��30 �6    6.   �� 5      $��5 �6    6-   �� 5      $
��auto �6    60    � ColumnDefinitions 5      �� 5      6%   �� 5      6%   �� 5      6%   �� 5      6%   .+�5      �� -   5        $��	NameLabel��5      �� 5      -   5        $��KontostandLabel��5      $��3��6/   6A   �� 5      $"��Ihre Letzten 5 Transaktionen��6   $��1��6;   $��1��6K   6Z   &   System.Windows.Controls.DataGrid  5      -   5          Name$ LetzteTransaktion��5         AlternatingRowBackground
 ��·�   
IsReadOnly .    AutoGenerateColumns .  $��1��6�   $��2��6�   $��3��6�      CanUserAddRows .     CanUserDeleteRows .  $��0,0,20,0q�6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 TransaktionID��6)   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6@   �� *TransaktionID+6d     5       $	 Betrag��6)   
 69   �� *Amount+ �� StringFormat$ {0}€��6k     5!         Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6)   $	 	Kommentar��63   
 6F   �� *	Comment+6d     5"      $	 Datum��6)   
 68   �� *Date+$ 
dd.MM.yyyy��6l   5$      �� 5%      $��Konto auswählen:��6   $��1��6/   $��2��6<   6N   �� 5&      -   5         ��Name$ 
Kontoliste��5&      $��0,0,20,0q�6[   $��1��6m   $��3��6z   6�   �� 5'      $��3��6   $��2��6!   $��Kontostand:��61   6I   �� 5(      -   5         ��Name$ kontostandAnzeigen��5(   1   $��3��6   $��3��6!   6O   5*      � 5+   
     5,      �� 5-      $��45 �6    6.   �� 5.      $��45 �6    6.   �� 5/      $��45 �6    6.   �� 50      $��45 �6    6.   �� 51      $��45 �6    6.   �� 52      $��45 �6    6.    54      �� 55      6%   �� 56      6%   �� 57      6%   �� 58      6%   .+�5:      �� -   5        $��0��5:      $��Geld überweisen��6"   $��20,0,0,0q�6U   6j   �� 5;      -   5        $��0��5;      $��3��6"   $��Kontoauszug��62   $��0,0,20,0q�6`   6t   �� 5<      -   5        $��1��5<      $��
Einzahlung��6"   $��20,0,0,0q�6N   6c   �� 5=      -	   5        $��1��5=      $��3��6"   $��
Auszahlung��62   $��0,0,20,0q�6\   6q   �� 5>      -
   5        $��2��5>      $��3��6"   $��	Ausloggen��62   $��0,0,20,0q�6Y   6n   �� 5?      -   5        $��2��5?      $��Benutzerdaten anpassen��6"   $��20,0,0,0q�6\   6q   5A      5C      5D      