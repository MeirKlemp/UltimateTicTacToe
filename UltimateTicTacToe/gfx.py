class Graphics:
    def __init__(self, surface, pygame):
        self.pygame = pygame
        self.surface = surface

    def rect(self, x, y, width, height, color, line_thickness=0):
        self.pygame.draw.rect(self.surface, color, self.pygame.Rect(x, y, width, height), line_thickness)

    def circle(self, x, y, radius, color, line_thickness=0):
        self.pygame.draw.circle(self.surface, color, (x, y), radius, line_thickness)

    def ellipse(self, x, y, width, height, color, line_thickness=0):
        self.pygame.draw.ellipse(self.surface, color, self.pygame.Rect(x, y, width, height), line_thickness)

    def cake(self, x, y, width, height, color, start_angle, stop_angle, line_thickness=0):
        self.pygame.draw.arc(self.surface, color, self.pygame.Rect(x, y, width, height), start_angle, start_angle, line_thickness)

    def polygon(self, points, color, line_thickness=0):
        self.pygame.draw.polygon(self.surface, color, points, line_thickness)

    def line(self, start, end, color, thickness=1):
        self.pygame.draw.line(self.surface, color, start, end, thickness)

    def fill(self, color):
        self.surface.fill(color)
