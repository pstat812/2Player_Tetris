   M S B A M L   `   `   `  ���� A  =Tetris, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:TetrisGame.UITetrisGame.UI      TetrisGame.UI.GameView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -localclr-namespace:TetrisGame.UI     $	��700��$
��1100��  � 	Resources     BoolToVisibilityConverter��%   &	        �� .��� ���#2D2D30 � ColumnDefinitions �� $��3* ��� $��1* ��� $��3* ��� $��1* �.+��� -    ��Name$ GameCanvas1��$��0�����#1E1E1E$	��300��$	��600��$��10q��� $��Center=�$
��Left=�$	��193��$	��600��$��3��$��	380,0,0,0q�.+��� $��Score��$��24(�
�������$��0,0,0,10q�$��Center=�$
��Bold"��� $��24(�
��� ���$��0,0,0,20q�$��Center=����� *Player1Score+�� $
��Next��$��24(�
�������$��0,0,0,10q�$��Center=�$
��Bold"�%   TetrisGame.UI.NextTetrominoView  $��0,0,0,20q� � DataContext �� *Player1NextTetromino+�� $��	Controls:��$��16(�
�������$��0,14,0,5q�$��Center=�$
��Bold"�$��97���� $��A: Move Left��$��14(�
�������$��0,0,0,2q�$��Center=�$	��100���� $��D: Move Right��$��14(�
�������$��0,0,0,2q�$��Center=�$	��101���� $��	W: Rotate��$��14(�
�������$��0,0,0,2q�$��Center=�$	��103���� $��S: Soft Drop��$��14(�
�������$��0,0,0,2q�$��Center=�$	��100���� $��     LCTRL: Hard Drop��$��14(�
�������$��0,0,0,20q�$��Center=� J� RenderTransformOrigin$ 0.5,0.5&�$	��137��z�V� .~��� �� ��  �� Angle$ 0.016Q�T� �� -   $��Pause��$	��150��$��40��$��	0,50,0,10q����#3E3E42
�������$
��Bold"��� -   $ GameCanvas2��$��2�����#1E1E1E$	��300��$	��600��$��38,50,74,50q�y��� *IsTwoPlayer+ �� 	Converter# [  �� $��2��$��	357,0,0,0q�$��Center=�$
��Left=�$	��150��$��2��$	��600��y��� *IsTwoPlayer+# [  .+��� $��Score��$��24(�
�������$��0,0,0,10q�$��Center=�$
��Bold"��� $��24(�
��� ���$��0,0,0,20q�$��Center=����� *Player2Score+�� $
��Next��$��24(�
�������$��0,0,0,10q�$��Center=�$
��Bold"�  $��0,0,0,20q� �� *Player2NextTetromino+�� $��	Controls:��$��16(�
�������$��0,15,0,5q�$��Center=�$
��Bold"�$��88���� $��    Left: Move Left��$��14(�
�������$��0,0,0,2q�$	��123���� $��       Right: Move Right��$��14(�
�������$��0,0,0,2q�$��Center=�$	��145���� $��    Up: Rotate��$��14(�
�������$	��123���� $��     Down: SoftDrop��$��14(�
�������$��0,0,0,2q�$��Center=�$	��129���� $��       RCTRL: Hard Drop��$��14(�
�������$	��144��� -    �Name$ Overlay��$y�	Collapsed=����	#80000000$��4��.+�� -   $ 	PauseMenu��$y�	Collapsed=����	#80000000.+��� $��Center=�$��Center=�$	��300��$	��300�����#2D2D30 J� Opacity$	 0.9Q�.+��� $��Paused��$��36(�
�������$��Center=�$��	0,20,0,20q�$
��Bold"��� -   $��Resume��$	��200��$��40��$��50,10,50,10q����#3E3E42
�������$
��Bold"��� -   $��Restart��$	��200��$��40��$��50,10,50,10q����#3E3E42
�������$
��Bold"��� -   $��	Back Menu��$	��200��$��40��$��50,10,50,10q����#3E3E42
�������$
��Bold"�� -	   $ GameOverMenu��$y�	Collapsed=����	#80000000.+��� $��Center=�$��Center=�$	��300��$	��350�����#2D2D30$
 0.95Q�.+��� $�� ��$��36(�
�������$��Center=�$��	0,20,0,10q�$
��Bold"��� $��24(�
�������$��Center=�$��0,0,0,10q����� *Winner+�� -
   $��Restart��$	��200��$��40��$��
50,55,50,5q����#3E3E42
�������$
��Bold"��� -   $��	Back Menu��$	��200��$��40��$��	50,5,50,5q����#3E3E42
�������$
��Bold"�