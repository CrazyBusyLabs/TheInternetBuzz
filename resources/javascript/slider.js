(function() {
    var contentSlider;
    var contentSliderWidth;
    var contentSliderInner;
    var contentSliderMaximumLeft;
    var inProgress = false;

    $(window).load(function() {
        contentSlider = $('div.contentSlider');
        contentSliderWidth = contentSlider.outerWidth();
        contentSliderInner = $('ul', contentSlider);
        contentSliderMaximumLeft = contentSlider.outerWidth() - contentSliderInner.innerWidth();

        $(".contentSliderLeftButton").click(function() {
            if (!inProgress) {
                var position = contentSliderInner.position();
                if (position.left < 0) {
                    inProgress = true;
                    contentSliderInner.animate({ left: "+=" + contentSliderWidth + "px" }, 500, function() { inProgress = false; });
                }
            }
        });

        $(".contentSliderRightButton").click(function() {
            if (!inProgress) {
                var position = contentSliderInner.position();
                if (position.left > contentSliderMaximumLeft) {
                    inProgress = true;
                    contentSliderInner.animate({ left: "-=" + contentSliderWidth + "px" }, 500, function() { inProgress = false; });
                }
            }
        });
    });
})();