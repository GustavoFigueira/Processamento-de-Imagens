   M S B A M L   `   `   `  ���� S  OProcessamento de Imagens, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Processamento_de_ImagensProcessamento_de_Imagens  )    #Processamento_de_Imagens.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     8local&clr-namespace:Processamento_de_Imagens       � Title$2  ,Processamento de Imagens - Redimensionamento��$	��800��$
��1200��.��� $��0,0,0,17q� � ColumnDefinitions �� $
��0.5* ��� $
��0.5* � � RowDefinitions �� $	��80* ��� $
��Auto �$��38���� $��30 ��� $
��367* �.+�� $��0��$��0��$��2�� �� $
��0.3* ��� $
��0.3* ��� $
��0.3* �.+��� -    ��Name$
 Logo��$��0��$��Center=�$��Uniform=�$��Center=�$��Assets/uft-logo.png��$��154,24,162,26q�$��54��$��81���� $��1��$��0��$��Center=�$��Center=� �� TextAlignment$ Center=�$
��Bold"�$��20(��� $�Processamento de Imagens���� �� $�	Trabalhos���� $��2��$��0��$��Right=�$��0,0,20,0q�$��Center=�$��14(�$��Italic$�$��Light"�$ Right=�97Desenvolvido por: Gustavo Figueira Lopes e João Celino�� IDE: Visual Studio 2018��  Linguagem de Programação: C#�� $��0��$��Right=�$��1��$��2��$��
0,10,145,2q� �� Orientation$ 
Horizontal=�.+��� -    ��Name$ Option��$��Stretch=�$	��150��$��20,0,0,0q�$��Stretch=�.@��� $��Ampliação���� $��	Redução���� $��Equalização Histograma���� $5��/Transformação de Intensidade (Limiarização)���� $/��)Transformação de Intensidade (Negativo)���� $'��!Operação Aritmética (Adição)���� $*��$Operação Aritmética (Subtração)���� $(��"Operação Geométrica (Rotação)���� $��Rotulação���� -    ��Name$ Scaling��$y�Hidden=�$ 
Horizontal=�.+��� $!��Valor de redimensionamento:��$��20,0,0,0q��� -    ��Name$ Amount��	 � 
InputScope$	 Number��$	��100��$��2��	� -   
 	�Name$
 NearestNeighbor��$��Vizinho mais próximo�� d� 	IsChecked$
 TrueY� 	� 	GroupName$
 Mode��$��Stretch=�$��Stretch=�$��20,0,0,0q����	#FF5C1EFF	� -   $
 Bilinear��$��Bilinear��$
 Mode��$��Stretch=�$��Stretch=�$��20,0,0,0q��� -   $ Thresholding��$y�Hidden=�.+��� -    ��Name$ InvertThreshold��$ FalseY�$��Inverter���� -	   $ 	Aritmetic��$y�Hidden=��� -
   $ 	Geometric��$y�Hidden=�.+�	� -   $
 Rotation��$��	Rotação��$
 TrueY�$
 Mode��$��Stretch=�$��Stretch=�$��20,0,0,0q����	#FF5C1EFF�� -    ��Name$ Generate��$��Gerar��$��20,0,0,0q����	#7F2E8DEC$��Right=�$��60���� -   $ Clean��$��Limpar��$��20,0,0,0q����	#7FEC2E2E$��Right=�$��60���� $��0��$��3��$��1�����	#FFE6E6E6$��	20,0,40,0q�.+��� -   $ ImageViewerBefore��$��Center=�$��Center=�$	��500���� $��1��$��3�����	#FFE6E6E6$��	40,0,20,0q�.+��� -   $��ImageViewerAfter��$��Center=�$��Center=�$��Uniform=�$	��500��