import 'bootstrap/dist/css/bootstrap.css';
import 'fancybox/dist/css/jquery.fancybox.css';
import 'owl-carousel/owl-carousel/owl.carousel.css';
import 'font-awesome/css/font-awesome.css';
import 'jquery-uniform/themes/default/css/uniform.default.css';
import 'jquery.rateit/scripts/rateit.css';
import { Cart } from './cart';

$ = window.$ = window.jQuery = require('jquery');
window.Cart = Cart;

require('fancybox')($);
require('jquery-slimscroll/jquery.slimscroll.js');
require('owl-carousel/owl-carousel/owl.carousel.js');
Cart.get();