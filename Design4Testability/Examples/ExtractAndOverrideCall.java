//extract & override call
public class PageLayout
{
    private int id = 0;
    private List styles;
    private StyleTemplate template;
    ...
    protected void rebindStyles() {
        styles = StyleMaster.formStyles(template, id);
        ...
    }
    ...
}

//after the extraction
public class PageLayout
{
    private int id = 0;
    private List styles;
    private StyleTemplate template;
    ...
    protected void rebindStyles() {
        styles = formStyles(template, id);
        ...
    }

    protected List formStyles(StyleTemplate template,
                             int id) {
        return StyleMaster.formStyles(template, id);
    }
    ...
}


//the testing class
public class TestingPageLayout extends PageLayout {
     protected List formStyles(StyleTemplate template,
                             int id) {
        return new ArrayList();
    }
    ...
}
