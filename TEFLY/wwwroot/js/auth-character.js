/**
 * Tefly auth mascot — eye tracking, shy password pose, idle blink, happy state.
 * Requires partial _AuthCharacter.cshtml and auth-character.css.
 */
(function () {
  'use strict';

  const MASCOT_SELECTOR = '[data-tefly-mascot]';
  const PASSWORD_SELECTOR = 'input[type="password"]';
  const BLINK_MIN_MS = 2800;
  const BLINK_MAX_MS = 5200;
  const PUPIL_ATTR = 'data-pupil';
  const EYE_ATTR = 'data-eye';

  function clamp(value, min, max) {
    return Math.min(max, Math.max(min, value));
  }

  function randomBetween(min, max) {
    return min + Math.random() * (max - min);
  }

  class TeflyMascot {
    constructor(root) {
      this.root = root;
      this.svg = root.querySelector('svg');
      this.head = root.querySelector('.tefly-mascot__head');
      this.pupils = Array.from(root.querySelectorAll(`[${PUPIL_ATTR}]`));
      this.eyes = Array.from(root.querySelectorAll(`[${EYE_ATTR}]`));
      this.shyCount = 0;
      this.maxHeadTilt = 6;
      this.maxPupilOffset = parseFloat(
        getComputedStyle(document.documentElement).getPropertyValue('--pupil-max')
      ) || 7;

      this.onPointerMove = this.onPointerMove.bind(this);
      this.onPasswordFocus = this.onPasswordFocus.bind(this);
      this.onPasswordBlur = this.onPasswordBlur.bind(this);
      this.scheduleBlink = this.scheduleBlink.bind(this);
    }

    init() {
      if (!this.svg || this.pupils.length === 0) return;

      document.addEventListener('pointermove', this.onPointerMove, { passive: true });
      this.bindPasswordFields();
      this.scheduleBlink();
      this.observeFormValidity();
    }

    destroy() {
      document.removeEventListener('pointermove', this.onPointerMove);
      if (this.blinkTimer) clearTimeout(this.blinkTimer);
      this._passwordUnbind?.();
    }

    bindPasswordFields() {
      const form = this.root.closest('form') || document;
      const fields = form.querySelectorAll(PASSWORD_SELECTOR);
      const handlers = [];

      fields.forEach((field) => {
        const focus = () => this.onPasswordFocus();
        const blur = () => this.onPasswordBlur();
        const enter = () => this.onPasswordFocus();
        const leave = () => {
          if (document.activeElement !== field) this.onPasswordBlur();
        };

        field.addEventListener('focus', focus);
        field.addEventListener('blur', blur);
        field.addEventListener('mouseenter', enter);
        field.addEventListener('mouseleave', leave);
        handlers.push({ field, focus, blur, enter, leave });
      });

      this._passwordUnbind = () => {
        handlers.forEach(({ field, focus, blur, enter, leave }) => {
          field.removeEventListener('focus', focus);
          field.removeEventListener('blur', blur);
          field.removeEventListener('mouseenter', enter);
          field.removeEventListener('mouseleave', leave);
        });
      };
    }

    onPasswordFocus() {
      this.shyCount += 1;
      this.root.classList.add('tefly-mascot--shy');
    }

    onPasswordBlur() {
      this.shyCount = Math.max(0, this.shyCount - 1);
      if (this.shyCount === 0) this.root.classList.remove('tefly-mascot--shy');
    }

    onPointerMove(event) {
      if (this.root.classList.contains('tefly-mascot--shy')) return;

      const pt = this.svg.createSVGPoint();
      pt.x = event.clientX;
      pt.y = event.clientY;
      const cursor = pt.matrixTransform(this.svg.getScreenCTM().inverse());

      this.pupils.forEach((pupil) => {
        const eyeId = pupil.getAttribute(PUPIL_ATTR);
        const eye = this.eyes.find((e) => e.getAttribute(EYE_ATTR) === eyeId);
        if (!eye) return;

        const cx = parseFloat(eye.getAttribute('data-cx'));
        const cy = parseFloat(eye.getAttribute('data-cy'));
        const dx = cursor.x - cx;
        const dy = cursor.y - cy;
        const dist = Math.hypot(dx, dy) || 1;
        const scale = Math.min(this.maxPupilOffset / dist, 1);

        pupil.setAttribute('transform', `translate(${dx * scale} ${dy * scale})`);
      });

      if (this.head) {
        const bbox = this.svg.getBoundingClientRect();
        const centerX = bbox.left + bbox.width / 2;
        const norm = clamp((event.clientX - centerX) / (bbox.width / 2), -1, 1);
        this.head.style.transform = `rotate(${norm * this.maxHeadTilt}deg)`;
      }
    }

    scheduleBlink() {
      if (window.matchMedia('(prefers-reduced-motion: reduce)').matches) return;

      this.blinkTimer = setTimeout(() => {
        if (!this.root.classList.contains('tefly-mascot--shy')) {
          this.root.classList.add('tefly-mascot--blink');
          setTimeout(() => {
            this.root.classList.remove('tefly-mascot--blink');
            this.scheduleBlink();
          }, 140);
        } else {
          this.scheduleBlink();
        }
      }, randomBetween(BLINK_MIN_MS, BLINK_MAX_MS));
    }

    observeFormValidity() {
      const form = this.root.closest('form');
      if (!form) return;

      const check = () => {
        const required = form.querySelectorAll('[required], input:not([type="hidden"])');
        let allFilled = true;
        required.forEach((el) => {
          if (el.type === 'checkbox') return;
          if (el.offsetParent === null) return;
          if (!el.value || (el.validity && !el.validity.valid)) allFilled = false;
        });
        this.root.classList.toggle('tefly-mascot--happy', allFilled && form.checkValidity());
      };

      form.addEventListener('input', check);
      form.addEventListener('change', check);
      check();
    }
  }

  function initAll() {
    document.querySelectorAll(MASCOT_SELECTOR).forEach((el) => {
      if (el._teflyMascot) return;
      const mascot = new TeflyMascot(el);
      mascot.init();
      el._teflyMascot = mascot;
    });
  }

  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', initAll);
  } else {
    initAll();
  }
})();
