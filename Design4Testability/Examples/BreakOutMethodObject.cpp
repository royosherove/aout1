//break Out Method Object example

class GDIBrush
{
public:
    void draw(vector<point>& roots,
              ColorMatrix& colors,
              vector<point>& selection);
    ...

private:
    void drawPoint(int x, int y, COLOR color);
    ...

}

void GDIBrush::draw(vector<point>& roots, ColorMatrix& colors,
                    				vector<point>& selection)
	{
	    for(vector<points>::iterator it = roots.begin();it != roots.end();++it) 
	    	{
	        point p = *it;
	        ...
	
	        drawPoint(p.x, p.y, colors[n]);
		}
    	}

    ...



///create a method-related class with the same signature
class Renderer
{
public:
    Renderer(GBIBrush *brush,
             vector<point>& roots,
             ColorMatrix &colors,
             vector<point>& selection);
    ...
};



//all parameters become private members

class Renderer
{
private:
    GDIBrush *brush;
    vector<point>& roots;
    ColorMatrix& colors;
    vector<point>& selection;

public:
    Renderer(GDIBrush *brush,
             vector<point>& roots,
             ColorMatrix& colors,
             vector<point>& selection)
        
        {
        brush(brush); roots(roots);
          colors(colors); selection(selection);
        }
};




class Renderer
{
private:
    GDIBrush *brush;
    vector<point>& roots;
    ColorMatrix& colors;
    vector<point>& selection;

public:
    Renderer(GDIBrush *brush,vector<point>& roots, ColorMatrix& colors,
             					vector<point>& selection)
        : 
          colors(colors), selection(selection)
        {}

    void draw();
};




void Renderer::draw()
{
    for(vector<points>::iterator it = roots.begin();
            it != roots.end();
            ++it) {
        point p = *it;
        ...
        drawPoint(p.x, p.y, colors[n]);
    }
    ...
}




//Original class, refactored

void GDIBrush::draw(vector<point>& roots,
                    ColorMatrix &colors,
                    vector<point>& selection)
{
    Renderer renderer(this, roots,
                      colors, selection);
    renderer.draw();
}



class PointRenderer
{
    public:
        virtual void drawPoint(int x, int y, COLOR color) = 0;
};


class GDIBrush : public PointRenderer
{
public:
    void drawPoint(int x, int y, COLOR color);
    ...
};


class Renderer
{
private:
    PointRender *pointRenderer;
    vector<point>& roots;
    ColorMatrix& colors;
    vector<point>& selection;

public:
    Renderer(PointRenderer *renderer,
             vector<point>& roots,
             ColorMatrix& colors,
             vector<point>& selection)
        
        : pointRenderer(pointRenderer),
          roots(roots),
          colors(colors), selection(selection)
        {}

    void draw();
};

void Renderer::draw()
{
    for(vector<points>::iterator it = roots.begin();
            it != roots.end();
            ++it) {
        point p = *it;
        ...
        pointRenderer->drawPoint(p.x,p.y,colors[n]);
    }
    ...
}


