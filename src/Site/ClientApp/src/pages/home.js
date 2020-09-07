import 'owl-carousel/owl-carousel/owl.carousel.css';
require('owl-carousel/owl-carousel/owl.carousel.js');

$(document).ready(function () {
    $('.owl-carousel-home').owlCarousel({
        pagination: false,
        navigation: true,
        items: 5,
        lazyLoad: true,
        addClassActive: true,
        itemsCustom: [
            [0, 1],
            [320, 1],
            [480, 2],
            [660, 2],
            [700, 3],
            [768, 3],
            [992, 4],
            [1024, 4],
            [1200, 5],
            [1400, 5],
            [1600, 5]
        ]
    });
});