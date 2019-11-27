import math

class Graphics:
    def __init__(self, surface, pygame, gfxdraw):
        self.pygame = pygame
        self.surface = surface
        self.gfxdraw = gfxdraw

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

    def arc(self, x, y, r, start_angle, stop_angle, fill_color=(), line_color=(), line_thickness=0, smooth=1):
        if fill_color:
            self.pygame.draw.arc(self.surface, fill_color, self.pygame.Rect(x, y, width, height), start_angle, stop_angle)
        if line_thickness > 0 and line_color:
            #self.pygame.draw.arc(self.surface, line_color, self.pygame.Rect(x, y, width, height), start_angle, stop_angle, line_thickness)
            #self.gfxdraw.arc(self.surface, int(x + width / 2), int(y + height / 2), int(height / 2), int(start_angle * 180 / pi), int(stop_angle * 180 / pi), line_color)
            points = []
            for phi in range(start_angle, stop_angle, smooth):
                a = r * math.cos(phi * math.pi / 180)
                b = r * math.sin(phi * math.pi / 180)
                points.append((x + a, y + b))
            if len(points) > 1:
                self.pygame.draw.lines(self.surface, line_color, False, points, line_thickness)


    def polygon(self, points, fill_color=(), line_color=(), line_thickness=0):
        if fill_color:
            self.pygame.draw.polygon(self.surface, fill_color, points)
        if line_thickness > 0 and line_color:
            self.pygame.draw.polygon(self.surface, line_color, points, line_thickness)

    def line(self, start, end, color, thickness=1):
        self.pygame.draw.line(self.surface, color, start, end, thickness)

    def background(self, color):
        self.surface.fill(color)
