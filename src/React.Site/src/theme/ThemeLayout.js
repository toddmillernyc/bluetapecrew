// ReSharper disable SuspiciousThisUsage
export class ThemeLayout {
    constructor($) {
        this.$ = $
        this.jQuery = $
    }
    $
    jQuery

    responsiveHandlers = []
    responsive = true

    init() {
        this.handleTheme()
        this.handleInit()
        this.handleResponsiveOnResize()
        this.handleFancybox()
        this.handleDifInits()
        this.handleSidebarMenu(window.jQuery)
        this.handleAccordions()
        this.handleMenu()
        //this.handleScrollers()
        this.handleSubMenuExt(window.jQuery)
        this.handleMobiToggler(window.jQuery)
    }

    initTwitter() {
        ((d, s, id) => {
            var fjs = d.getElementsByTagName(s)[0], p = /^http:/.test(d.location.toString()) ? 'http' : 'https'
            if (!d.getElementById(id)) {
                const js = d.createElement(s);
                js.id = id;
                js.src = p + '://platform.twitter.com/widgets.js'
                const parentNode = fjs.parentNode;
                if (parentNode != null) parentNode.insertBefore(js, fjs);
            }
        })(document, 'script', 'twitter-wjs');
    }

    initTouchspin() {
        this.$('.product-quantity .form-control').TouchSpin({
            buttondown_class: 'btn quantity-down',
            buttonup_class: 'btn quantity-up'
        })
        this.$('.quantity-down').html("<i class='fa fa-angle-down'></i>")
        this.$('.quantity-up').html("<i class='fa fa-angle-up'></i>")
    }

    initUniform() {
        const $ = this.$
        const jQuery = this.jQuery

        if (jQuery().uniform) return;
        const test = $('input[type=checkbox]:not(.toggle), input[type=radio]:not(.toggle, .star)');
        if (test.size() > 0) {
            test.each(() => {
                const self = $(this);
                if (self.parents('.checker').size() === 0) {
                    self.show();
                    self.uniform();
                }
            });
        }
    }

    handleTheme() {
        var panel = this.$('.color-panel')

        var setColor = (color) => {
            this.$('#style-color').attr('href', `../../assets/corporate/css/themes/${color}.css`)
            this.$('.corporate .site-logo img').attr('src', `../../assets/corporate/img/logos/logo-corp-${color}.png`)
            this.$('.ecommerce .site-logo img').attr('src', `../../assets/corporate/img/logos/logo-shop-${color}.png`)
        }

        this.$('.icon-color', panel).click(() => {
            this.$('.color-mode').show()
            this.$('.icon-color-close').show()
        })

        this.$('.icon-color-close', panel).click(() => {
            this.$('.color-mode').hide()
            this.$('.icon-color-close').hide()
        })

        this.$('li', panel).click(() => {
            const self = this.$(this)
            const color = self.attr('data-style')
            setColor(color)
            this.$('.inline li', panel).removeClass('current')
            self.addClass('current')
        })
    }

    handleInit = () => {
        if (!!navigator.userAgent.match(/MSIE 10.0/)) {
            this.jQuery('html').addClass('ie10') // detect IE10 version
        }
        if (!!navigator.userAgent.match(/MSIE 11.0/)) {
            this.jQuery('html').addClass('ie11') // detect IE11 version
        }
    }

    handleResponsiveOnResize() {

        var runResponsiveHandlers = (responsiveHandlers) => {
            // reinitialize other subscribed elements
            for (let i in responsiveHandlers) {
                if (Object.prototype.hasOwnProperty.call(responsiveHandlers, i)) {
                    const each = responsiveHandlers[i]
                    each.call()
                }
            }
        }

        var resize
        this.$(window).resize(() => {
            if (resize) {
                clearTimeout(resize)
            }
            resize = setTimeout(() => {
                runResponsiveHandlers(this.responsiveHandlers)
            }, 50) // wait 50ms until window resize finishes.
        })
    }

    handleFancybox(jQuery) {
        if (!window.jQuery.hasOwnProperty('fancybox')) {
            return
        }

        jQuery('.fancybox-fast-view').fancybox()

        if (jQuery('.fancybox-button').size() > 0) {
            jQuery('.fancybox-button').fancybox({
                groupAttr: 'data-rel',
                prevEffect: 'none',
                nextEffect: 'none',
                closeBtn: true,
                helpers: {
                    title: {
                        type: 'inside'
                    }
                }
            })
        }
    }

    handleDifInits() {
        this.$('.header .navbar-toggle span:nth-child(2)').addClass('short-icon-bar')
        this.$('.header .navbar-toggle span:nth-child(4)').addClass('short-icon-bar')
    }

    handleSidebarMenu($) {
        $('.sidebar .dropdown > a').click((event) => {
            var self = $(this)
            if (self.next().hasClass('dropdown-menu')) {
                event.preventDefault()
                if (self.hasClass('collapsed') === false) {
                    self.addClass('collapsed')
                    self.siblings('.dropdown-menu').slideDown(300)
                } else {
                    self.removeClass('collapsed')
                    self.siblings('.dropdown-menu').slideUp(300)
                }
            }
        })
    }

    handleAccordions() {
        this.jQuery('body').on('shown.bs.collapse', '.accordion.scrollable', (e) => {
            this.scrollTo(this.$(e.target), -100, this.$)
        })
    }

    scrollTo(el, offSet, $) {
        let pos = (el && el.size() > 0) ? el.offset().top : 0
        if (el) {
            if ($('body').hasClass('page-header-fixed')) {
                pos = pos - $('.header').height()
            }
            pos = pos + (offSet ? offSet : -1 * el.height())
        }

        this.jQuery('html,body').animate({
            scrollTop: pos
        }, 'slow')
    }

    handleMenu() {
        this.$('.header .navbar-toggle').click(() => {
            if (this.$('.header .navbar-collapse').hasClass('open')) {
                this.$('.header .navbar-collapse').slideDown(300)
                    .removeClass('open')
            } else {
                this.$('.header .navbar-collapse').slideDown(300)
                    .addClass('open')
            }
        })
    }

    handleScrollers() {
        var scrollers = window.jQuery('.scroller')
        console.log(scrollers)
        if(scrollers.length > 0){
            scrollers.each(() => {
                var self = window.jQuery(this)
                var height
                if (self.attr('data-height')) {
                    height = self.attr('data-height')
                } else {
                    height = self.css('height')
                }
                self.slimScroll({
                    allowPageScroll: true, // allow page scroll when the element scroll is ended
                    size: '7px',
                    color: (self.attr('data-handle-color') ? self.attr('data-handle-color') : '#bbb'),
                    railColor: (self.attr('data-rail-color') ? self.attr('data-rail-color') : '#eaeaea'),
                    position: 'right',
                    height: height,
                    alwaysVisible: (self.attr('data-always-visible') === '1'),
                    railVisible: (self.attr('data-rail-visible') === '1'),
                    disableFadeOut: true
                })
            })
        }

    }

    handleSubMenuExt($) {
        $('.header-navigation .dropdown').on('hover', () => {
            var self = $(this)
            if (self.children('.header-navigation-content-ext').show()) {
                if ($('.header-navigation-content-ext').height() >= $('.header-navigation-description').height()) {
                    $('.header-navigation-description').css('height', $('.header-navigation-content-ext').height() + 22);
                }
            }
        });
    }

    handleMobiToggler() {
        this.$('.mobi-toggler').on('click', (event) => {
            event.preventDefault();//the default action of the event will not be triggered
            this.$('.header').toggleClass('menuOpened')
            this.$('.header').find('.header-navigation').toggle(300)
        })
    }
}