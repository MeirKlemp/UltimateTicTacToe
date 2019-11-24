class Graphics:
    def __init__(self, surface, pygame):
        self.pygame = pygame
        self.surface = surface

    def rect(self, x, y, width, height, fill_color=(), line_color=(), line_thickness=0):
        if fill_color:
            self.pygame.draw.rect(self.surface, fill_color, self.pygame.Rect(x, y, width, height))
        if line_thickness > 0 and line_color:
            self.pygame.draw.rect(self.surface, line_color, self.pygame.Rect(x, y, width, height), line_thickness)

    def circle(self, x, y, radius, fill_color=(), line_color=(), line_thickness=0):
        if fill_color:
            self.pygame.draw.circle(self.surface, fill_color, (x, y), radius)
        if line_thickness > 0 and line_color:
            self.pygame.draw.circle(self.surface, line_color, (x, y), radius, line_thickness)

    def ellipse(self, x, y, width, height, fill_color=(), line_color=(), line_thickness=0):
        if fill_color:
            self.pygame.draw.ellipse(self.surface, fill_color, self.pygame.Rect(x, y, width, height))
        if line_thickness > 0 and line_color:
            self.pygame.draw.ellipse(self.surface, line_color, self.pygame.Rect(x, y, width, height), line_thickness)

    def cake(self, x, y, width, height, start_angle, stop_angle, fill_color=(), line_color=(), line_thickness=0):
        if fill_color:
            self.pygame.draw.arc(self.surface, fill_color, self.pygame.Rect(x, y, width, height), start_angle, stop_angle)
        if line_thickness > 0 and line_color:
            self.pygame.draw.arc(self.surface, line_color, self.pygame.Rect(x, y, width, height), start_angle, stop_angle, line_thickness)

    def polygon(self, points, fill_color=(), line_color=(), line_thickness=0):
        if fill_color:
            self.pygame.draw.polygon(self.surface, fill_color, points)
        if line_thickness > 0 and line_color:
            self.pygame.draw.polygon(self.surface, line_color, points, line_thickness)

    def line(self, start, end, color, thickness=1):
        self.pygame.draw.line(self.surface, color, start, end, thickness)

    def background(self, color):
        self.surface.fill(color)
