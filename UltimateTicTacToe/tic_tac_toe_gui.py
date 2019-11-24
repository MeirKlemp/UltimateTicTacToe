from gui import GuiObject, animations
from math import tau

class TicTacToeGui(GuiObject):

    def __init__(self, x, y, width, height, tictactoe):

        GuiObject.__init__(self, x, y, width, height)

        self.tictactoe = tictactoe
        self.win_anim = animations.Animation(500)
        self.animations = []
        for row in range(self.tictactoe.rows):
            self.animations.append([])
            for col in range(self.tictactoe.cols):
                self.animations[row].append(animations.Animation(500))

    def mouse_down(self, x, y):
        if self.tictactoe.game_over():
            self.enabled = False
        else:
            row = int(y // (self.height / self.tictactoe.rows))
            col = int(x // (self.width / self.tictactoe.cols))

            if self.tictactoe.play(row, col):
                self.animations[row][col].start()
            if self.tictactoe.win_pos:
                self.win_anim.start()

    def update(self, window):
        pass

    def draw(self, gfx):
        # background
        gfx.background((255, 255, 255))

        # drawing seperation line
        if self.tictactoe.winner == self.tictactoe.NOUGHT:
            color = (255, 0, 0)
        elif self.tictactoe.winner == self.tictactoe.CROSS:
            color = (0, 0, 255)
        elif self.tictactoe.winner == self.tictactoe.EMPTY:
            color = (0, 255, 0)
        else:
            color = (0, 0, 0)

        hori = self.height / self.tictactoe.rows
        for row in range(1, self.tictactoe.rows):
            gfx.line((0, hori * row), (self.width, hori * row), color, 3)
        verti = self.width / self.tictactoe.cols
        for col in range(1, self.tictactoe.cols):
            gfx.line((verti * col, 0), (verti * col, self.height), color, 3)

        # drawing noughts and crosses
        space = .05
        width = self.width / self.tictactoe.cols
        height = self.height / self.tictactoe.rows

        for row in range(self.tictactoe.rows):
            for col in range(self.tictactoe.cols):
                if self.tictactoe.board[row][col] != self.tictactoe.EMPTY:
                    x = self.width / self.tictactoe.cols * col
                    x += width * space
                    y = self.height / self.tictactoe.rows * row
                    y += height * space

                    w = width * (1 - space * 2)
                    h = height * (1 - space * 2)
                    anim_value = self.animations[row][col].value()
                    if self.tictactoe.board[row][col] == self.tictactoe.NOUGHT:
                        gfx.cake(x, y, w, h, 0, tau * anim_value, line_color=(255, 0, 0), line_thickness=3)
                    else:
                        gfx.line((x, y), (x + w * anim_value, y + h * anim_value), (0, 0, 255), 3)
                        gfx.line((x + w, y), (x + w * (1 - anim_value), y + h * anim_value), (0, 0, 255), 3)

        # drawing win line
        if self.tictactoe.win_pos:
            pass
