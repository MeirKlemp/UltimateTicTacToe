from gui import GuiObject, animations
from math import tau

class TicTacToeGui(GuiObject):

    @staticmethod
    def draw_nought(gfx, x, y, width, height, color, anim_value=1, thickness=3):
        gfx.arc(x + width / 2, y + height / 2, width / 2, 0, int(360 * anim_value), line_color=color, line_thickness=thickness)

    @staticmethod
    def draw_cross(gfx, x, y, width, height, color, anim_value=1, thickness=3):
        gfx.line((x, y), (x + width * anim_value, y + height * anim_value), color, thickness)
        gfx.line((x + width, y), (x + width * (1 - anim_value), y + height * anim_value), color, thickness)

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
            enabled = False
        else:
            row = int(y // (self.height / self.tictactoe.rows))
            col = int(x // (self.width / self.tictactoe.cols))

            if self.tictactoe.play(row, col):
                self.animations[row][col].start()
            if self.tictactoe.winner:
                self.win_anim.start()

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
                        self.draw_nought(gfx, x, y, w, h, (255, 0, 0), anim_value)
                    else:
                        self.draw_cross(gfx, x, y, w, h, (0, 0, 255), anim_value)

        # drawing win line
        if self.win_anim.started():
            if self.tictactoe.winner == self.tictactoe.CROSS:
                self.draw_cross(gfx, self.x, self.y, self.height, self.width, (0, 0, 255), self.win_anim.value(), 100)
            else:
                self.draw_nought(gfx, self.x, self.y, self.height, self.width, (255, 0, 0), self.win_anim.value(), 100)
