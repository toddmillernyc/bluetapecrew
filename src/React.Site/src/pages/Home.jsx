import React, { useEffect, useState } from 'react';
import 'owlcarousel/owl-carousel/owl.carousel.css'

export default (props) => {

    const [catagories, setCatagories] = useState([])
    const [baseUrl, setBaseUrl] = useState('')

    useEffect(() => {

        async function init() {
            setBaseUrl('https://localhost:5001/')
            const response = await fetch('https://localhost:5001/catalog')
            const json = await response.json()
            setCatagories(json)
            initOWL()
            //init image zoon
            //window.jQuery('.product-main-image').zoom({ url: window.jQuery('.product-main-image img').attr('data-BigImgSrc') });
        }
        
        init()
    }, []);

    function initOWL() {
        window.jQuery('.owl-carousel6-brands').owlCarousel({
            pagination: false,
            navigation: true,
            items: 6,
            addClassActive: true,
            itemsCustom: [
                [0, 1],
                [320, 1],
                [480, 2],
                [700, 3],
                [975, 5],
                [1200, 6],
                [1400, 6],
                [1600, 6]
            ]
        })

        window.jQuery('.owl-carousel-home').owlCarousel({
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
    }

    return(
    <div className="main" id="home-page">
        <div className="container">
            {catagories.map((category) => { return (
                <div className="row margin-bottom-40" id="allProducts" key={category.categoryName}>
                    <div className="col-md-12 sale-product">
                        <h2>{category.categoryName}</h2>
                        <div className="owl-carousel owl-carousel-home">
                            {category.products.map((item, index) => {
                                return(
                                <div key={item.name}>
                                    <div className="product-item">
                                        <div className="pi-img-wrapper" id={`product-image-wrapper-${index}`}>
                                            <img 
                                                className="lazyOwl img-responsive"
                                                data-src={`${baseUrl}${item.imgSource}`}
                                                src={`${baseUrl}${item.imgSource}`}
                                                alt={item.name}
                                            />
                                            <div>
                                                <a 
                                                    href={`images/${item.slug}.jpg`}
                                                    className="btn btn-default fancybox-button"
                                                    id={`fancybox-button-${index}`}>
                                                    <i style={{'color': 'white' }} className="fa fa-expand"></i>
                                                </a>
                                            </div>
                                        </div>
                                        <h3>
                                            <a href={`${baseUrl}products/${item.name}`}>{item.name}</a>
                                        </h3>
                                        <div className="pi-price">${item.price}</div>
                                        <a 
                                            href={`products/${item.Slug}`}
                                            className="btn btn-default add2cart"
                                            id={`product-details-link-${index}`}
                                        >
                                            Details
                                        </a>
                                    </div>
                                </div>   
                            )})}    
                        </div>
                    </div>
                </div>
            )})}
        </div>
    </div>
)}