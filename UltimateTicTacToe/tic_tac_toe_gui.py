from gui import GuiObject, animations
from tic_tac_toe import TicTacToe
from math import tau

class TicTacToeGui(GuiObject):

    def __init__(self, x, y, width, height, tictactoe):

        GuiObject.__init__(self, x, y, width, height)

        self.tictactoe = tictactoe
        self.animations = []
        for row in range(self.tictactoe.rows):
            self.animations.append([])
            for col in range(self.tictactoe.cols):
                self.animations[row].append(animations.Animation(500))

    def mouse_down(self, x, y):
        row = int(y // (self.height / self.tictactoe.rows))
        col = int(x // (self.width / self.tictactoe.cols))

        if self.tictactoe.play(row, col):
            self.animations[row][col].start()

    def update(self, window):
        pass

    def draw(self, gfx):
        # background
        gfx.background((255, 255, 255))

        # drawing seperation line
        hori = self.height / self.tictactoe.rows
        for row in range(1, self.tictactoe.rows):
            gfx.line((0, hori * row), (self.width, hori * row), (0, 0, 0), 3)
        verti = self.width / self.tictactoe.cols
        for col in range(1, self.tictactoe.cols):
            gfx.line((verti * col, 0), (verti * col, self.height), (0, 0, 0), 3)

        # drawing noughts and crosses
        
        space = .05
        width = self.width / self.tictactoe.cols
        height = self.height / self.tictactoe.rows

        for row in range(self.tictactoe.rows):
            for col in range(self.tictactoe.cols):
                if self.tictactoe.board[row][col] != TicTacToe.EMPTY:
                    x = self.width / self.tictactoe.cols * col
                    x += width * space
                    y = self.height / self.tictactoe.rows * row
                    y += height * space

                    w = width * (1 - space * 2)
                    h = height * (1 - space * 2)
                    anim_value = self.animations[row][col].value()
                    if self.tictactoe.board[row][col] == TicTacToe.NOUGHT:
                        gfx.cake(x, y, w, h, (255, 255, 255), (255, 0, 0), 0, tau * anim_value, 3)
                    else:
                        gfx.line((x, y), (x + w * anim_value, y + h * anim_value), (0, 0, 255), 3)
                        gfx.line((x + w, y), (x + w * (1 - anim_value), y + h * anim_value), (0, 0, 255), 3)
