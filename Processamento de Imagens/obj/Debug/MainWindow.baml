   M S B A M L   `   `   `  ����S  OProcessamento de Imagens, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Processamento_de_ImagensProcessamento_de_Imagens  5   	   )    #Processamento_de_Imagens.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:Processamento_de_Imagens     5   	     � Title$2  ,Processamento de Imagens - Redimensionamento��5   	   $	��800��6>   $
��1200��6K   .��5	      � $��0,0,0,17q�6    � ColumnDefinitions 5
   
   �� 5      $
��0.5* �6   �� 5      $
��0.5* �6    � RowDefinitions 5   
   �� 5      $
��119* �6   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��547* �6   .+�5   
   � $��0��6   $��0��6   $��2��6,    5      �� 5      $
��0.3* �6#   �� 5      $
��0.3* �6#   �� 5      $
��0.3* �6#   .+�5      �� -   5         ��Name$
 Logo��5   $   $��0��6   $��Center=�62   $��Uniform=�6O   $��Center=�6b   $��Assets/uft-logo.png��6}   $��154,24,162,26q�6�   $��54��6�   $��81��6�   �� 5      $��1��6   $��0��6(   $��Center=�65   $��Center=�6P    �� TextAlignment$ Center=�6m   $
��Bold"�6�   $��20(�6�   �� 6�   $�Processamento de Imagens��6�   �� 6�   �� 6�   $�	Trabalhos��6�   6�   �� 5      $��2��6   $��0��6(   $��Right=�65   $��0,0,20,0q�6Q   $��Center=�6c   $��14(�6~   $��Italic$�6�   $��Light"�6�   $ Right=�6�   97Desenvolvido por: Gustavo Figueira Lopes e João Celino�� 5      IDE: Visual Studio 2018�� 6B    Linguagem de Programação: C#5      5      �� 5    
   $��0��6   $��Right=�6%   $��1��6A   $��2��6N   $��	0,10,20,2q�6b    �� Orientation$ 
Horizontal=�6u   $
��1152��6�   .+�5"      �� -   5         ��Name$ Option��5"      $	��100��6'   $��20,0,0,0q�63   .@�5#      �� $��Ampliação��6   �� 5$      $��	Redução��6   �� 5%      $��Equalização Histograma��6   �� 5&      $5��/Transformação de Intensidade (Limiarização)��6   �� 5'      $/��)Transformação de Intensidade (Negativo)��6   �� 5(      $'��!Operação Aritmética (Adição)��6   �� 5)      $*��$Operação Aritmética (Subtração)��6   �� 5*      $(��"Operação Geométrica (Rotação)��6   �� 5+      $��Rotulação��6   �� 5,      $��Filtro de Média��6   �� 5-      $��Filtro Laplaciano��6   �� 5.      $��Filtro Gradiente (Sobel)��6   �� 5/      $��Dilatação��6   �� 50      $��Erosão��6   �� 51      $��Abertura��6   �� 52      $��
Fechamento��6   53      �� 54      -   5         ��Name$ Scaling��54      $y�Hidden=�6*   $ 
Horizontal=�6>   .+�55      �� $!��Valor de redimensionamento:��6   $��20,0,0,0q�6>   �� 56      -   5         ��Name$ Amount��56      	 � 
InputScope$	 Number��6*   $	��100��6>   $��2��6J   	� 57      -   5        
 	�Name$
 NearestNeighbor��57      $��Vizinho mais próximo��67    d� 	IsChecked$
 TrueY�6V    	� 	GroupName$
 Mode��6g   $��Stretch=�6x   $��Stretch=�6�   $��20,0,0,0q�6�   ���	#FF5C1EFF6�   	� 58      -   5        $
 Bilinear��58      $��Bilinear��60   $
 Mode��6C   $��Stretch=�6T   $��Stretch=�6r   $��20,0,0,0q�6�   59      �� 5:      -   5        $ Thresholding��5:   -   $y�Hidden=�6   .+�5;      �� -   5         ��Name$ InvertThreshold��5;      $ FalseY�64   $��Inverter��6F   5<      �� 5=      -	   5        $ MedianFilter��5=   -   $y�Hidden=�6   .+�5>      �� $��Matriz:��6   $��20,0,0,0q�6*   �� 5?      -
   5        $ MedianFilterMatrix��5?      $	 Number��66   $	��100��6J   $��3��6V   5@      �� 5A      -   5        $ LaplacianFilter��5A   -   $y�Hidden=�6   .+�5B      �� -   5        $ LaplacianOption��5B      $��Stretch=�64   $��20,0,0,0q�6R   $	��100��6d   $��Stretch=�6p   .@�5C      �� $	��3X3��6#   �� 5D      $	��5X5��6#   �� 5E      $��	Gaussiano��6#   �� 5F      $��	3X3 e 5X5��6#   5G      5H      �� 5I      -   5        $ 	Geometric��5I   -   $y�Hidden=�6   .+�5J      	� -   5        $
 Rotation��5J      $��	Rotação��60   $
 TrueY�6B   $
 Mode��6S   $��Stretch=�6d   $��Stretch=�6�   $��20,0,0,0q�6�   ���	#FF5C1EFF6�   5K      �� 5L      -   5         ��Name$ Generate��5L      $��Gerar��6'   $��20,0,0,0q�67   ���	#7F2E8DEC$��Right=�6w   $��60��6�   �� 5M      -   5        $ Clean��5M      $��Limpar��6$   $��20,0,0,0q�65   ���	#7FEC2E2E$��Right=�6^   $��60��6z   6�   5N      �� 5O   
   $��0��6   $��3��6%   $��1��62   ���	#FFE6E6E6$��	20,0,40,0q�6]   .+�5P      �� -   5        $ ImageViewerBefore��5P      $��Center=�6/   $��Center=�6L   $	��500��6g   5Q      �� 5R   
   $��1��6   $��3��6%   ���	#FFE6E6E6$��	40,0,20,0q�6I   .+�5S      �� -   5        $��ImageViewerAfter��5S      $��Center=�6,   $��Center=�6I   $��Uniform=�6d   $	��500��6v   5T      5U      5V      