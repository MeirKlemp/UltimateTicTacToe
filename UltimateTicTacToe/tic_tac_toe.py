class TicTacToe:
    CROSS = 1
    NOUGHT = -1
    EMPTY = 0

    def __init__(self, rows=3, cols=3, win_set=3):

        if not isinstance(rows, TicTacToe):

            if rows <= 0 or cols <= 0 or win_set <= 0:
                raise Exception("rows, cols and win_set must be greater than zero.")
            if win_set > max(rows, cols):
                raise Exception("win_set must be less than rows and cols.")

            self.rows = rows
            self.cols = cols
            self.win_set = win_set
            self.moves_count = 0
            self.board = [([self.EMPTY] * self.cols) for _ in range(self.rows)]
        else:
            clone = rows
            self.rows = clone.rows
            self.cols = clone.cols
            self.win_set = clone.win_set
            self.moves_count = clone.moves_count
            self.board = [([self.EMPTY] * self.cols) for _ in range(self.rows)]

            for row in range(self.rows):
                for col in range(self.cols):
                    self.board[row][col] = clone.board[row][col]

    def set(self, row, col, position):
        if self.in_bounds(row, col) and position != self.EMPTY and self.board[row][col] == self.EMPTY:
            self.board[row][col] = position
            self.moves_count += 1
            return True
        return False

    def play(self, row, col):
        position = self.turn_position()
        return self.set(row, col, position)

    def turn_position(self):
        return self.CROSS if self.moves_count % 2 == 0 else self.NOUGHT

    def in_bounds(self, row, col):
        return 0 <= row < self.rows and 0 <= col < self.cols

    def clone(self):
        return TicTacToe(self)
